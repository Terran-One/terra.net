using Newtonsoft.Json;
using Terra.Sdk.Lcd;
using Terra.Sdk.Lcd.Models;

void Dump(object value)
{
    var jsonSerializerSettings = new JsonSerializerSettings {ReferenceLoopHandling = ReferenceLoopHandling.Ignore};
    var json = JsonConvert.SerializeObject(value, Formatting.Indented, jsonSerializerSettings);
    Console.WriteLine(json);
}

var client = new LcdClient(new LcdClientConfig
{
    Url = "https://bombay-lcd.terra.dev"
});

var res = await client.Market.GetSwapRate(new Coin("umnt", 61153995M), "umyr");
Console.WriteLine("***Result***");
Dump(res);
