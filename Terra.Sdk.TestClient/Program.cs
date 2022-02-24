using Terra.Sdk.Lcd;
using Terra.Sdk.Lcd.Models;

var lcdClientConfig = new LcdClientConfig();
var queryParams = new QueryParams();

var lcdClient = new LcdClient(lcdClientConfig);

var result1 = await lcdClient.Bank.Total(queryParams);
var value1 = result1.Value;

var result2 = await result1.NextPage();
var value2 = result2.Value;

var result3 = await result2.NextPage();
var value3 = result3.Value;
