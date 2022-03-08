using Newtonsoft.Json;
using Terra.Sdk.Lcd;

var client = new LcdClient(new LcdClientConfig
{
    Url = "https://fcd.terra.dev"
});

// var txInfo = await client.Tx.GetTxInfo("6E0C34D677D49E7D17A37D6866F9914172E6AFBE2E6E36DC181B7170F106AB20");
// var txInfos = await client.Tx.GetTxInfosByHeight(long.Parse(txInfo.Value.Height));
// Console.WriteLine(JsonConvert.SerializeObject(txInfos, Formatting.Indented, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }));

var tx = (await client.Tx.GetTxInfo("6E0C34D677D49E7D17A37D6866F9914172E6AFBE2E6E36DC181B7170F106AB20")).Value.Tx;
var encoded = client.Tx.Encode(tx);
var decoded = client.Tx.Decode(encoded);
Console.WriteLine(JsonConvert.SerializeObject(decoded, Formatting.Indented, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }));

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
