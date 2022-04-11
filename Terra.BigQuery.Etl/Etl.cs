using System.Data;
using System.Diagnostics;
using Google;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Bigquery.v2.Data;
using Google.Cloud.BigQuery.V2;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using Npgsql;
using Terra.Sdk.Lcd.Models.Entities.Tx.Msg;

namespace Terra.BigQuery.Etl;

public static class Etl
{
    private static BigQueryClient BigQueryClient { get; } = BigQueryClient.Create("minerva-341810", GoogleCredential.FromFile(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "bq.json")));

    public static async Task CreateTable(string db)
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
        await BigQueryClient.CreateTableAsync(db, "tx", schema);
    }

    public static async Task InsertData(string host, string db, int? batchSize, int? offset, int? limit)
    {
        await using var pgConnection = new NpgsqlConnection($"host={host};database=fcd;user id=fcd;password=terran.one;");

        var offsetClause = offset.HasValue ? $" OFFSET {offset}" : "";
        var limitClause = limit.HasValue ? $" LIMIT {limit}" : "";
        var pgCommand = new NpgsqlCommand($"SELECT hash, data FROM public.tx {offsetClause}{limitClause};", pgConnection);

        pgConnection.Open();

        var messageDeserializer = MessageDeserializer.Get();
        var noMessageDefined = new HashSet<string>();

        var bqTable = await BigQueryClient.GetTableAsync(db, "tx");

        batchSize ??= 1;
        var i = 1;
        var batch = new List<BigQueryInsertRow>();

        var pgReader = Profile("Postgres", () => pgCommand.ExecuteReader());
        while (Profile("Postgres", () => pgReader.Read()))
        {
            if (++i % batchSize == 0)
            {
                var success = true;
                for (var retries = 0; retries < 5; retries++)
                {
                    try
                    {
                        await ProfileAsync("BigQuery", () => bqTable.InsertRowsAsync(batch));
                        break;
                    }
                    catch (GoogleApiException e)
                    {
                        Console.WriteLine($"Insert failed ({e.Message}) - retry {retries + 1} of 5...");
                        success = false;
                    }
                }

                batch.Clear();

                if (!success)
                    Console.WriteLine("No more retries");

                Console.WriteLine($"{DateTime.Now}: {i} rows processed");
            }

            string json = null;
            try
            {
                var dataRecord = (IDataRecord) pgReader;
                var hash = (string) dataRecord[0];
                json = (string) dataRecord[1];

                var data = Profile("Deserialize JSON", () => JsonConvert.DeserializeAnonymousType(
                    json,
                    new {Tx = new {Value = new {Msg = new[] {new {Type = "", Value = new JObject()}}}}},
                    new JsonSerializerSettings
                    {
                        ContractResolver = new DefaultContractResolver
                        {
                            NamingStrategy = new SnakeCaseNamingStrategy()
                        }
                    }));

                var messages = data.Tx.Value.Msg
                    .Select(m => messageDeserializer.Deserialize(m.Type.Split('/')[1], m.Value))
                    .Select(t =>
                    {
                        var nestedField = Profile("Row builder codegen", () => NestedField.Create(t.Item2));
                        var insertRow = Profile("Row builder execution", () => nestedField?.BuildInsertRow(t.Item1));
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

                batch.Add(new BigQueryInsertRow
                {
                    {"TxHash", hash},
                    {"Messages", messages},
                    {"Timestamp", DateTime.Now.AsBigQueryDate()}
                });
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

        Console.WriteLine();
        Console.WriteLine("| Step                    | Average Duration per batch (ms) |");
        Console.WriteLine("|-------------------------| --------------------------------|");
        foreach (var (step, durations) in ProfileResults)
        {
            var durationPerIteration = Math.Round(durations.Aggregate((a, b) => a + b) / (decimal) durations.Count, 4);
            var durationPerBatch = step == "BigQuery" ? durationPerIteration : durationPerIteration * batchSize;
            Console.WriteLine($"| {step,-23} | {durationPerBatch, -31} |");
        }
        Console.WriteLine();
    }

    private static readonly IDictionary<string, List<long>> ProfileResults = new Dictionary<string, List<long>>();

    private static T Profile<T>(string key, Func<T> func)
    {
        var sw = new Stopwatch();
        sw.Start();
        var result = func();
        sw.Stop();

        if (!ProfileResults.TryGetValue(key, out var durations))
        {
            durations = new List<long>();
            ProfileResults.Add(key, durations);
        }

        durations.Add(sw.ElapsedMilliseconds);
        return result;
    }

    private static async Task<T> ProfileAsync<T>(string key, Func<Task<T>> func)
    {
        var sw = new Stopwatch();
        sw.Start();
        var result = await func();
        sw.Stop();

        if (!ProfileResults.TryGetValue(key, out var durations))
        {
            durations = new List<long>();
            ProfileResults.Add(key, durations);
        }

        durations.Add(sw.ElapsedMilliseconds);
        return result;
    }
}
