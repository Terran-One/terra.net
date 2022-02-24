using System;
using System.Linq;
using Terra.Sdk.Lcd;
using Terra.Sdk.Lcd.Models;

var config = new LcdClientConfig
{
    Url = "https://fcd.terra.dev",
    ChainId = "columbus-5"
};
var lcdClient = new LcdClient(config);

var queryParams = new QueryParams
{
    IsDescending = true,
    GetTotalCount = true
};

var result1 = await lcdClient.Bank.Total(queryParams);
var value1 = result1.Value;
Console.WriteLine($"Page#1: {string.Join("; ", value1.Select(c => $"{c.Denom} {c.Amount}"))}");

var result2 = await result1.NextPage();
var value2 = result2.Value;
Console.WriteLine($"Page#2: {string.Join("; ", value2.Select(c => $"{c.Denom} {c.Amount}"))}");

var result3 = await result2.NextPage();
var value3 = result3.Value;
Console.WriteLine($"Page#3: {string.Join("; ", value3.Select(c => $"{c.Denom} {c.Amount}"))}");
