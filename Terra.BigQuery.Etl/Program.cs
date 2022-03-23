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

if (args.Length < 2)
{
    Console.WriteLine("Usage:");
    Console.WriteLine("  dotnet run create|insert DB_HOST [OFFSET]");
    return;
}

var client = BigQueryClient.Create("minerva-341810", GoogleCredential.FromFile(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "bq.json")));

var command = args[0];
var host = args[1];
var offset = args.Length >= 3 ? int.Parse(args[3]) : 0;

switch (command)
{
    case "create":
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
        await client.CreateTableAsync("fcd3", "tx", schema);
        return;

    case "insert":
        break;

    default:
        Console.WriteLine("Valid commands are create, insert");
        return;
}

await using var pgConnection = new NpgsqlConnection($"host={host};database=fcd;user id=fcd;password=terran.one;");
var pgCommand = new NpgsqlCommand($"SELECT hash, data FROM public.tx OFFSET {offset};", pgConnection);
pgConnection.Open();

var messageDeserializer = MessageDeserializer.Get();
var noMessageDefined = new HashSet<string>();

var bqTable = await client.GetTableAsync("fcd3", "tx");

var i = 1;
var pgReader = pgCommand.ExecuteReader();
while (pgReader.Read())
{
    if (++i % 10 == 0)
        Console.WriteLine($"{DateTime.Now}: {i} rows processed");

    string json = null;
    try
    {
        var dataRecord = (IDataRecord) pgReader;
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
                if (insertRow != null)
                    return new BigQueryInsertRow {{"Type", t.Item3}, {t.Item3, insertRow}};

                if (!noMessageDefined.Contains(t.Item3))
                {
                    Console.WriteLine($"No message class defined for {t.Item3}");
                    Console.WriteLine($"Data: {json}");
                    noMessageDefined.Add(t.Item3);
                }

                return new BigQueryInsertRow {{"Type", t.Item3}};

            })
            .ToList();

        var row = new BigQueryInsertRow
        {
            {"TxHash", hash},
            {"Messages", messages},
            {"Timestamp", DateTime.Now.AsBigQueryDate()}
        };
        bqTable.InsertRow(row);
    }
    catch (Exception e)
    {
        Console.WriteLine(e);
        Console.WriteLine($"Data: {json}");
    }
}

await pgReader.DisposeAsync();
await pgCommand.DisposeAsync();
await pgConnection.DisposeAsync();
