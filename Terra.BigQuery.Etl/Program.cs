using Google.Apis.Auth.OAuth2;
using Google.Apis.Bigquery.v2.Data;
using Google.Cloud.BigQuery.V2;
using Newtonsoft.Json;
using Terra.BigQuery.Etl;
using Terra.Sdk.Lcd.Models;
using Terra.Sdk.Lcd.Models.Entities.Tx.Msg;
using Terra.Sdk.Lcd.Models.Entities.Tx.Msg.BankMsg;
using Terra.Sdk.Lcd.Models.Entities.Tx.Msg.MsgAuthMsg;
using Terra.Sdk.Lcd.Models.Entities.Tx.Msg.WasmMsg;

void Dump(object value)
{
    var jsonSerializerSettings = new JsonSerializerSettings {ReferenceLoopHandling = ReferenceLoopHandling.Ignore};
    var json = JsonConvert.SerializeObject(value, Formatting.Indented, jsonSerializerSettings);
    Console.WriteLine(json);
}

var client = BigQueryClient.Create("minerva-341810", GoogleCredential.FromJson(@"MY_JSON"));
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
table.InsertRow(new BigQueryInsertRow
{
    { "TxHash", "bc43a6eeb36382ad8e75ac97c9f525404211b3f428a13b0e51aae6149d24de7e" },
    { "Messages", new[]
        {
            new BigQueryInsertRow
            {
                { "Type", "MsgMultiSend" },
                {
                    "MsgMultiSend",
                    NestedField.Create(typeof(MsgMultiSend)).BuildInsertRow(new MsgMultiSend
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
                    })
                }
            },
            new BigQueryInsertRow
            {
                { "Type", "MsgExecuteContract" },
                {
                    "MsgExecuteContract",
                    NestedField.Create(typeof(MsgExecuteContract)).BuildInsertRow(new MsgExecuteContract
                    {
                        Coins = new List<Coin>
                        {
                            new("uuid", 10M),
                            new("uuid", 20M),
                        },
                        Contract = "this_is_a_contract",
                        Sender = "my_sender",
                        ExecuteMsg = new MsgExecAuthorized
                        {
                            Grantee = "my_grantee"
                        },
                        TypeUrl = "/terra.wasm.v1beta1.MsgExecuteContract"
                    })
                }
            }
        }
    },
    { "Timestamp", DateTime.Now.AsBigQueryDate() },
});

var query = "SELECT * FROM `minerva-341810.fcd3.tx` LIMIT 1";
var job = client.CreateQueryJob(query, null, new QueryOptions { UseQueryCache = false });
job.PollUntilCompleted();

foreach (var row in client.GetQueryResults(job.Reference))
    Dump(row);
