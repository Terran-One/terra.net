using Newtonsoft.Json;
using Terra.Sdk.Lcd;
using Terra.Sdk.Lcd.Api.Parameters;

void Dump(object value)
{
    var jsonSerializerSettings = new JsonSerializerSettings {ReferenceLoopHandling = ReferenceLoopHandling.Ignore};
    var json = JsonConvert.SerializeObject(value, Formatting.Indented, jsonSerializerSettings);
    Console.WriteLine(json);
}

var client = new LcdClient(new LcdClientConfig
{
    Url = "https://fcd.terra.dev"
});

// var txInfo = await client.Tx.GetTxInfo("6E0C34D677D49E7D17A37D6866F9914172E6AFBE2E6E36DC181B7170F106AB20");
// var txInfos = await client.Tx.GetTxInfosByHeight(long.Parse(txInfo.Value.Height));
// Console.WriteLine(JsonConvert.SerializeObject(txInfos, Formatting.Indented, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }));

Console.WriteLine("\n***Request***");
var tx = (await client.Tx.GetTxInfo("6E0C34D677D49E7D17A37D6866F9914172E6AFBE2E6E36DC181B7170F106AB20")).Value.Tx;
Dump(tx);

Console.WriteLine("\n***Response***");
var res = await client.Tx.Create(
    tx.AuthInfo.SignerInfos.Select(si => new SignerOptions {PublicKey = si.PublicKey, SequenceNumber = si.Sequence}).ToList(),
    new CreateTxOptions
    {
        Fee = tx.AuthInfo.Fee,
        Memo = tx.Body.Memo,
        Msgs = tx.Body.Messages
    });
Dump(res);

Console.WriteLine("\n***Broadcast***");
res.Value.Signatures = tx.Signatures;
var broadcast = await client.Tx.BroadcastSync(res.Value);
Dump(broadcast);

// var res = await client.Tx.EstimateFee(
//     tx.AuthInfo.SignerInfos.Select(si => new SignerData {PublicKey = si.PublicKey, SequenceNumber = si.Sequence}).ToList(),
//     new CreateTxOptions
//     {
//         Fee = tx.AuthInfo.Fee,
//         Memo = tx.Body.Memo,
//         Msgs = tx.Body.Messages
//     });
// Dump(res);

// var result1 = await lcdClient.Auth.GetAccount("terra1ll7lc3m0yt2eg0z7ntn5w9rdskxrrgd82ac75u");
// Console.WriteLine(JsonConvert.SerializeObject(result1));

// var result1 = await lcdClient.Bank.GetSupply(isDescending: true, getTotalCount: true);
// Console.WriteLine($"Page#1: {JsonConvert.SerializeObject(result1)}");
//
// var result2 = await lcdClient.Bank.GetSupply(isDescending: true, getTotalCount: true, paginationKey: result1.NextPageKey);
// Console.WriteLine($"Page#2: {JsonConvert.SerializeObject(result2)}");
//
// var result3 = await lcdClient.Bank.GetSupply(isDescending: true, getTotalCount: true, paginationKey: result2.NextPageKey);
// Console.WriteLine($"Page#3: {JsonConvert.SerializeObject(result3)}");

