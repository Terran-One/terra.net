using Newtonsoft.Json;
using Terra.Sdk.Lcd;

void Dump(object value)
{
    var jsonSerializerSettings = new JsonSerializerSettings {ReferenceLoopHandling = ReferenceLoopHandling.Ignore};
    var json = JsonConvert.SerializeObject(value, Formatting.Indented, jsonSerializerSettings);
    Console.WriteLine(json);
}

var client = new LcdClient(new LcdClientConfig
{
    //Url = "https://bombay-lcd.terra.dev"
    Url = "https://fcd.terra.dev"
});

var res = await client.Wasm.GetContractQuery<>("terra1p54hc4yy2ajg67j645dn73w3378j6k05v52cnk");
Console.WriteLine("***Result***");
Dump(res);
