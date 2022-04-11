using Terra.Sdk.Lcd;
using Terra.Sdk.TestClient;

var client = new LcdClient(new LcdClientConfig
{
    //Url = "https://bombay-lcd.terra.dev"
    Url = "https://fcd.terra.dev"
});

var txInfo = await client.Tx.GetTxInfo("6E0C34D677D49E7D17A37D6866F9914172E6AFBE2E6E36DC181B7170F106AB20");
txInfo.Dump();
