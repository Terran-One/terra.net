using System.Collections.Generic;
using Terra.Sdk.Lcd;
using Terra.Sdk.Lcd.Models;

var lcdClientConfig = new LcdClientConfig();
var pagination = new PaginationOptions();
var apiParams = new Dictionary<string, object>();

// Approach #1
var lcdClient = new LcdClient(lcdClientConfig);
var result1 = await lcdClient.Bank.Total(pagination, apiParams);
var coins1 = result1.Value;

// Approach #2
lcdClientConfig.Pagination = pagination;
lcdClientConfig.ApiParams = apiParams;
var result2 = await Coin.Total(lcdClientConfig);
var coins2 = result2.Value;
