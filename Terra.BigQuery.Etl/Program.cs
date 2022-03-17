using Newtonsoft.Json;
using Terra.BigQuery.Etl;
using Terra.Sdk.Lcd.Models;
using Terra.Sdk.Lcd.Models.Entities.Tx.Msg.BankMsg;

void Dump(object value)
{
    var jsonSerializerSettings = new JsonSerializerSettings {ReferenceLoopHandling = ReferenceLoopHandling.Ignore};
    var json = JsonConvert.SerializeObject(value, Formatting.Indented, jsonSerializerSettings);
    Console.WriteLine(json);
}

var row = NestedField.Create(typeof(MsgMultiSend));

Console.WriteLine("*** Code ***");
Console.WriteLine(row.GeneratedCode);

if (!row.Success)
    return;

Console.WriteLine("*** Schema ***");
Dump(row.Schema);

Console.WriteLine("*** Data ***");
Dump(row.BuildInsertRow(new MsgMultiSend
{
    Inputs = new List<MsgMultiSend.Input>
    {
        new() {Address = "my_address_1", Coins = new List<Coin> { new("uuid", 10M) }},
        new() {Address = "my_address_2", Coins = new List<Coin> { new("uuid", 20M) }}
    },
    Outputs = new List<MsgMultiSend.Output>
    {
        new() {Address = "my_address_1", Coins = new List<Coin> { new("uuid", 10M) }},
        new() {Address = "my_address_2", Coins = new List<Coin> { new("uuid", 20M) }}
    },
    TypeUrl = "/cosmos.bank.v1beta1.MsgMultiSend"
}));

