using System;
using Newtonsoft.Json;
using Terra.Sdk.Lcd;

var lcdClient = new LcdClient(new LcdClientConfig
{
    Url = "https://fcd.terra.dev"
});

var result1 = await lcdClient.Account.Get("terra1ll7lc3m0yt2eg0z7ntn5w9rdskxrrgd82ac75u");
Console.WriteLine($"Page#3: {JsonConvert.SerializeObject(result1)}");

// var result1 = await lcdClient.Coin.GetAll(isDescending: true, getTotalCount: true);
// var value1 = result1.Value;
// Console.WriteLine($"Page#1: {string.Join("; ", value1.Select(c => $"{c.Denom} {c.Amount}"))}");
//
// var result2 = await lcdClient.Coin.GetAll(isDescending: true, getTotalCount: true, paginationKey: result1.NextPageKey);
// var value2 = result2.Value;
// Console.WriteLine($"Page#2: {string.Join("; ", value2.Select(c => $"{c.Denom} {c.Amount}"))}");
//
// var result3 = await lcdClient.Coin.GetAll(isDescending: true, getTotalCount: true, paginationKey: result2.NextPageKey);
// var value3 = result3.Value;
// Console.WriteLine($"Page#3: {string.Join("; ", value3.Select(c => $"{c.Denom} {c.Amount}"))}");
