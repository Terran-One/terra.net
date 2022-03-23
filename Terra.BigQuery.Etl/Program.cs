using System.Data;
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

BigQueryTable table;
if (Environment.GetEnvironmentVariable("ASSUME_CREATED")?.ToLowerInvariant() == "true")
{
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
    table = await client.GetOrCreateTableAsync("fcd3", "tx", schema);
}
else
{
    table = await client.GetTableAsync("fcd3", "tx");
}

await using var connection = new NpgsqlConnection($"host={Environment.GetEnvironmentVariable("FCD_HOST")};database=fcd;user id=fcd;password=terran.one;");
var offset = args.Length == 0 ? 0 : int.Parse(args[0]);
var command = new NpgsqlCommand($"SELECT hash, data FROM public.tx OFFSET {offset};", connection);
connection.Open();

var messageDeserializer = MessageDeserializer.Get();

var i = 1;
var reader = command.ExecuteReader();
while (reader.Read())
{
    if (++i % 10 == 0)
        Console.WriteLine($"{DateTime.Now}: {i} rows processed");

    string json = null;
    try
    {
        var dataRecord = (IDataRecord) reader;
        var hash = (string) dataRecord[0];
        json = (string) dataRecord[1];

        var data = JsonConvert.DeserializeAnonymousType(
            json,
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
            .Select(t =>
            {
                var insertRow = NestedField.Create(t.Item2)?.BuildInsertRow(t.Item1);
                return insertRow == null
                    ? new BigQueryInsertRow {{"Type", t.Item3}}
                    : new BigQueryInsertRow {{"Type", t.Item3}, {t.Item3, insertRow}};
            })
            .ToList();

        var row = new BigQueryInsertRow
        {
            {"TxHash", hash},
            {"Messages", messages},
            {"Timestamp", DateTime.Now.AsBigQueryDate()}
        };
        table.InsertRow(row);
    }
    catch (Exception e)
    {
        Console.WriteLine(e);
        Console.WriteLine($"Data: {json}");
    }
}

await reader.DisposeAsync();
await command.DisposeAsync();
await connection.DisposeAsync();
