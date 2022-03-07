using System;
using Newtonsoft.Json;
using Terra.Sdk.Lcd;

var lcdClient = new LcdClient(new LcdClientConfig
{
    Url = "https://fcd.terra.dev"
});

var res = await lcdClient.Tx.GetTxInfo("69CA62F1328A3FBC810A3E370A186BC2A5FAED2739848CA3336580DD17C58F7E");
Console.WriteLine(JsonConvert.SerializeObject(res));

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
