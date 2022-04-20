using System.Data;
using System.Globalization;
using Google;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Bigquery.v2.Data;
using Google.Cloud.BigQuery.V2;
using Npgsql;

namespace Terra.BigQuery.Etl;

public static class Etl
{
    private static readonly string BqKeyDirectory = Environment.GetEnvironmentVariable("BQ_KEY_DIR") ?? Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
    private static BigQueryClient BigQueryClient { get; } = BigQueryClient.Create("minerva-341810", GoogleCredential.FromFile(Path.Combine(BqKeyDirectory, "bq.json")));

    public static async Task CreateTable(string db)
    {
        var schema = new TableSchema
        {
            Fields = new List<TableFieldSchema>
            {
                new() {Name = "PgId", Type = "INT64", Mode = "REQUIRED"},
                new() {Name = "ChainId", Type = "STRING", Mode = "REQUIRED"},
                new() {Name = "BlockId", Type = "INT64", Mode = "REQUIRED"},
                new() {Name = "TxHash", Type = "STRING", Mode = "REQUIRED"},
                new() {Name = "Messages", Type = "STRING", Mode = "REQUIRED"},
                new() {Name = "Timestamp", Type = "DATETIME", Mode = "REQUIRED"}
            }
        };
        await BigQueryClient.CreateTableAsync(db, "tx", schema);
    }

    public static async Task InsertData(string host, string db, int? batchSize, long? minId, long? maxId)
    {
        var bqTable = await BigQueryClient.GetTableAsync(db, "tx");

        batchSize ??= 1;
        var i = 1;
        var batch = new List<BigQueryInsertRow>();

        await using var pgConnection = new NpgsqlConnection($"host={host};database=fcd;user id=fcd;password=terran.one;");
        pgConnection.Open();

        if (!minId.HasValue)
        {
            var dtQuery = await BigQueryClient.ExecuteQueryAsync($"SELECT MAX(PgId) AS Id FROM {bqTable.Reference.ProjectId}.{bqTable.Reference.DatasetId}.{bqTable.Reference.TableId}", Array.Empty<BigQueryParameter>());
            var dtEnumerator = dtQuery.GetRowsAsync().GetAsyncEnumerator();
            minId = await dtEnumerator.MoveNextAsync()
                ? dtEnumerator.Current["Id"] as long?
                : null;
        }

        var pgCommand = new NpgsqlCommand("SELECT id, chain_id, block_id, hash, data FROM public.tx", pgConnection);
        var clauses = new List<string>();

        if (minId.HasValue)
        {
            clauses.Add("id > (:MIN_ID)");
            pgCommand.Parameters.AddWithValue("MIN_ID", minId);
        }

        if (maxId.HasValue)
        {
            clauses.Add("id < (:MAX_ID)");
            pgCommand.Parameters.AddWithValue("MAX_ID", maxId);
        }

        if (clauses.Any())
            pgCommand.CommandText += $" WHERE {string.Join(" AND ", clauses)};";
        else
            pgCommand.CommandText += ";";

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

            try
            {
                var dataRecord = (IDataRecord) pgReader;
                var pgId = dataRecord[0];
                var chainId = dataRecord[1];
                var blockId = dataRecord[2];
                var hash = dataRecord[3];
                var messages = dataRecord[4];

                batch.Add(new BigQueryInsertRow
                {
                    {"PgId", pgId},
                    {"ChainId", chainId},
                    {"BlockId", blockId},
                    {"TxHash", hash},
                    {"Messages", messages},
                    {"Timestamp", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss", CultureInfo.InvariantCulture)}
                });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        await pgReader.DisposeAsync();
        await pgCommand.DisposeAsync();
        await pgConnection.DisposeAsync();
    }
}
