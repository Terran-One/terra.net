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
    //Url = "https://bombay-lcd.terra.dev"
    Url = "https://fcd.terra.dev"
});

var tx = (await client.Tx.GetTxInfo("89470DCB62DA9AB69E261D18A754AA0024729FADC5785D09066A2C6F30D2D3E5")).Value.Tx;
Console.WriteLine("***Request***");
Dump(tx);

var res = await client.Tx.EstimateFee(
    tx.AuthInfo.SignerInfos.Select(s => new SignerData
    {
        PublicKey = s.PublicKey,
        SequenceNumber = s.Sequence
    }).ToList(),
    new CreateTxOptions
    {
        Fee = tx.AuthInfo.Fee,
        Msgs = tx.Body.Messages,
        GasAdjustment = 0.1M
    }); //EstimateGas(tx, 6250373);
Console.WriteLine("***Result***");
Dump(res);

// var res = await client.Gov.GetDeposits(5333, 6250373);
// Console.WriteLine("***Result***");
// Dump(res);
