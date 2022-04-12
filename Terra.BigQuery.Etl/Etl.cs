using System.Data;
using Google;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Bigquery.v2.Data;
using Google.Cloud.BigQuery.V2;
using Npgsql;

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
                new() {Name = "Messages", Type = "STRING", Mode = "REQUIRED"},
                new() {Name = "Timestamp", Type = "DATETIME", Mode = "REQUIRED"}
            }
        };
        await BigQueryClient.CreateTableAsync(db, "tx", schema);
    }

    public static async Task InsertData(string host, string db, int? batchSize, int? offset, int? limit)
    {
        var bqTable = await BigQueryClient.GetTableAsync(db, "tx");

        batchSize ??= 1;
        var i = 1;
        var batch = new List<BigQueryInsertRow>();

        await using var pgConnection = new NpgsqlConnection($"host={host};database=fcd;user id=fcd;password=terran.one;");
        pgConnection.Open();

        var offsetClause = offset.HasValue ? $" OFFSET {offset}" : "";
        var limitClause = limit.HasValue ? $" LIMIT {limit}" : "";
        var pgCommand = new NpgsqlCommand($"SELECT hash, data FROM public.tx {offsetClause}{limitClause};", pgConnection);

        var pgReader = pgCommand.ExecuteReader();
        while (pgReader.Read())
        {
            if (++i % batchSize == 0)
            {
                try
                {
                    await bqTable.InsertRowsAsync(batch);
                }
                catch (GoogleApiException e)
                {
                    Console.WriteLine($"Batch insert failed ({e.Message}) - retrying with smaller batches...");

                    var smallBatchSize = batchSize.Value / 10;
                    for (var j = 0; j <= batchSize / smallBatchSize; j += smallBatchSize)
                    {
                        Console.WriteLine($"Inserting mini-batch {j + 1}...");
                        try
                        {
                            await bqTable.InsertRowsAsync(batch.Skip(i * smallBatchSize).Take(smallBatchSize));
                        }
                        catch (GoogleApiException ee)
                        {
                            Console.WriteLine($"Mini-batch insert failed ({ee.Message}) - retrying with smaller batches...");
                        }
                    }
                }

                batch.Clear();
                Console.WriteLine($"{DateTime.Now}: {i} rows processed");
            }

            string json = null;
            try
            {
                var dataRecord = (IDataRecord) pgReader;
                var hash = (string) dataRecord[0];
                json = (string) dataRecord[1];

                batch.Add(new BigQueryInsertRow
                {
                    {"TxHash", hash},
                    {"Messages", json},
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

        // Console.WriteLine();
        // Console.WriteLine("| Step                    | Average Duration per batch (ms) |");
        // Console.WriteLine("|-------------------------| --------------------------------|");
        // foreach (var (step, durations) in ProfileResults)
        // {
        //     var durationPerIteration = Math.Round(durations.Aggregate((a, b) => a + b) / (decimal) durations.Count, 4);
        //     var durationPerBatch = step == "BigQuery" ? durationPerIteration : durationPerIteration * batchSize;
        //     Console.WriteLine($"| {step,-23} | {durationPerBatch, -31} |");
        // }
        // Console.WriteLine();
    }

    // private static readonly IDictionary<string, List<long>> ProfileResults = new Dictionary<string, List<long>>();

    // private static T Profile<T>(string key, Func<T> func)
    // {
    //     var sw = new Stopwatch();
    //     sw.Start();
    //     var result = func();
    //     sw.Stop();
    //
    //     if (!ProfileResults.TryGetValue(key, out var durations))
    //     {
    //         durations = new List<long>();
    //         ProfileResults.Add(key, durations);
    //     }
    //
    //     durations.Add(sw.ElapsedMilliseconds);
    //     return result;
    // }
    //
    // private static async Task<T> ProfileAsync<T>(string key, Func<Task<T>> func)
    // {
    //     var sw = new Stopwatch();
    //     sw.Start();
    //     var result = await func();
    //     sw.Stop();
    //
    //     if (!ProfileResults.TryGetValue(key, out var durations))
    //     {
    //         durations = new List<long>();
    //         ProfileResults.Add(key, durations);
    //     }
    //
    //     durations.Add(sw.ElapsedMilliseconds);
    //     return result;
    // }
}
