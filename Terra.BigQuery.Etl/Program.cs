using Google;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Bigquery.v2.Data;
using Google.Cloud.BigQuery.V2;
using Newtonsoft.Json;
using Terra.BigQuery.Etl;
using Terra.Sdk.Lcd.Models;
using Terra.Sdk.Lcd.Models.Entities.Tx.Msg.BankMsg;
using Terra.Sdk.Lcd.Models.Entities.Tx.Msg.GovMsg;

void Dump(object value)
{
    var jsonSerializerSettings = new JsonSerializerSettings {ReferenceLoopHandling = ReferenceLoopHandling.Ignore};
    var json = JsonConvert.SerializeObject(value, Formatting.Indented, jsonSerializerSettings);
    Console.WriteLine(json);
}

var client = BigQueryClient.Create("minerva-341810", GoogleCredential.FromJson(@"JSON_GOES_HERE"));

var msgDeposit = NestedField.Create(typeof(MsgDeposit));
var msgSend = NestedField.Create(typeof(MsgSend));
var msgMultiSend = NestedField.Create(typeof(MsgMultiSend));
var schema = new TableSchema
{
    Fields = new List<TableFieldSchema>
    {
        new() {Name = "Id", Type = "INT64", Mode = "REQUIRED"},
        new() {Name = "ChainId", Type = "STRING", Mode = "REQUIRED"},
        new() {Name = "Hash", Type = "STRING", Mode = "REQUIRED"},
        new()
        {
            Name = "Messages", Type = "RECORD", Mode = "REPEATED",
            Fields = new List<TableFieldSchema>
            {
                new() {Name = "Type", Type = "STRING", Mode = "REQUIRED"},
                new() {Name = "MsgDeposit", Type = "RECORD", Mode = "NULLABLE", Fields = msgDeposit.Schema.Fields},
                new() {Name = "MsgSend", Type = "RECORD", Mode = "NULLABLE", Fields = msgSend.Schema.Fields},
                new() {Name = "MsgMultiSend", Type = "RECORD", Mode = "NULLABLE", Fields = msgMultiSend.Schema.Fields}
            }
        },
        new() {Name = "BlockId", Type = "INT64", Mode = "REQUIRED"},
        new() {Name = "Timestamp", Type = "DATETIME", Mode = "REQUIRED"},
    }
};

BigQueryTable table;
try
{
    table = await client.GetTableAsync("fcd", "tx");
}
catch (GoogleApiException)
{
    table = client.CreateTable("fcd", "tx", schema);
}

table.InsertRow(new BigQueryInsertRow
{
    { "Id", 3677429 },
    { "ChainId", "columbus-2" },
    { "Hash", "bc43a6eeb36382ad8e75ac97c9f525404211b3f428a13b0e51aae6149d24de7e" },
    { "Messages", new[]
        {
            new BigQueryInsertRow
            {
                { "Type", "MsgMultiSend" },
                { "MsgMultiSend", msgMultiSend.BuildInsertRow(new MsgMultiSend
                {
                    Inputs = new List<MsgMultiSend.Input>
                    {
                        new() {Address = "my_address_1", Coins = new List<Coin> {new("uuid", 10M)}},
                        new() {Address = "my_address_2", Coins = new List<Coin> {new("uuid", 20M)}}
                    },
                    Outputs = new List<MsgMultiSend.Output>
                    {
                        new() {Address = "my_address_1", Coins = new List<Coin> {new("uuid", 10M)}},
                        new() {Address = "my_address_2", Coins = new List<Coin> {new("uuid", 20M)}}
                    },
                    TypeUrl = "/cosmos.bank.v1beta1.MsgMultiSend"
                }) }
            }
        }
    },
    { "BlockId", 1257988 },
    { "Timestamp", DateTime.Now.AsBigQueryDate() },
});

var query = "SELECT * FROM `minerva-341810.fcd.tx` LIMIT 1";
var job = client.CreateQueryJob(query, null, new QueryOptions { UseQueryCache = false });
job.PollUntilCompleted();

foreach (var row in client.GetQueryResults(job.Reference))
    Dump(row);
