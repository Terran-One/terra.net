﻿using System.Data;
using Google;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Bigquery.v2.Data;
using Google.Cloud.BigQuery.V2;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using Npgsql;
using Terra.BigQuery.Etl;
using Terra.Sdk.Lcd.Models.Entities.Tx.Msg;

var client = BigQueryClient.Create("minerva-341810", GoogleCredential.FromFile(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "bq.json")));
var schema = new TableSchema
{
    Fields = new List<TableFieldSchema>
    {
        new() {Name = "TxHash", Type = "STRING", Mode = "REQUIRED"},
        new()
        {
            Name = "Messages", Type = "RECORD", Mode = "REPEATED",
            Fields = new[]
                {
                    new TableFieldSchema
                    {
                        Name = "Type",
                        Type = "STRING",
                        Mode = "REQUIRED"
                    }
                }.Concat(typeof(Msg).Assembly.GetTypes()
                    .Where(type => type.IsSubclassOf(typeof(Msg)))
                    .Select(type => new TableFieldSchema
                    {
                        Name = type.Name,
                        Type = "RECORD",
                        Mode = "NULLABLE",
                        Fields = NestedField.Create(type).Schema.Fields
                    }))
                .ToList()
        },
        new() {Name = "Timestamp", Type = "DATETIME", Mode = "REQUIRED"}
    }
};

var table = client.GetOrCreateTable("fcd3", "tx", schema); // the async version sometimes exits before the table is ready...

using var connection = new NpgsqlConnection("host=ec2-52-3-221-55.compute-1.amazonaws.com;database=fcd;user id=fcd;password=terran.one;");
var command = new NpgsqlCommand("SELECT hash, data FROM public.tx;", connection);
connection.Open();

var messageDeserializer = MessageDeserializer.Get();

var i = 1;
var reader = command.ExecuteReader();
while (reader.Read())
{
    if (++i % 10 == 0)
        Console.WriteLine($"{DateTime.Now}: {i} rows processed");

    var dataRecord = (IDataRecord) reader;
    var hash = (string) dataRecord[0];
    var data = JsonConvert.DeserializeAnonymousType(
        (string) dataRecord[1],
        new {Tx = new {Value = new {Msg = new[] {new {Type = "", Value = new JObject()}}}}},
        new JsonSerializerSettings
        {
            ContractResolver = new DefaultContractResolver
            {
                NamingStrategy = new SnakeCaseNamingStrategy()
            }
        });

    var messages = data.Tx.Value.Msg
        .Select(m => messageDeserializer.Deserialize(m.Type.Split('/')[1], m.Value))
        .Select(t => new BigQueryInsertRow
        {
            {"Type", t.Item3},
            {t.Item3, NestedField.Create(t.Item2)?.BuildInsertRow(t.Item1)}
        })
        .ToList();

    var row = new BigQueryInsertRow
    {
        {"TxHash", hash},
        {"Messages", messages},
        {"Timestamp", DateTime.Now.AsBigQueryDate()}
    };

    try
    {
        table.InsertRow(row);
    }
    catch (GoogleApiException e)
    {
        Console.WriteLine(e);
    }
}

reader.Close();
