using Newtonsoft.Json;
using Terra.Sdk.Lcd.Models;
using Terra.Sdk.Lcd.Models.Entities.Tx;
using Terra.Sdk.Lcd.Models.Entities.Tx.Msg;
using Terra.Sdk.Lcd.Models.Entities.Tx.Msg.BankMsg;
using Terra.Sdk.Lcd.Models.Signing;

void Dump(object value)
{
    var jsonSerializerSettings = new JsonSerializerSettings {ReferenceLoopHandling = ReferenceLoopHandling.Ignore};
    var json = JsonConvert.SerializeObject(value, Formatting.Indented, jsonSerializerSettings);
    Console.WriteLine(json);
}

// var client = new LcdClient(new LcdClientConfig
// {
//     //Url = "https://bombay-lcd.terra.dev"
//     Url = "https://fcd.terra.dev"
// });

// var tx = (await client.Tx.GetTxInfo("89470DCB62DA9AB69E261D18A754AA0024729FADC5785D09066A2C6F30D2D3E5")).Value.Tx;
// Console.WriteLine("***Request***");
// Dump(tx);

// var res = await client.Gov.GetDeposits(5333, 6250373);
// Console.WriteLine("***Result***");
// Dump(res);


var mk = new MnemonicKey(new MnemonicKeyOptions())
{
    Mnemonic = "island relax shop such yellow opinion find know caught erode blue dolphin behind coach tattoo light focus snake common size analyst imitate employ walnut"
};

var rk = new RawKey(mk.PrivateKey);
Console.WriteLine(rk.PublicKey.Key);
var accAddress = rk.AccAddress;

var msgSend = new MsgSend
{
    FromAddress = accAddress,
    ToAddress = "terra1wg2mlrxdmnnkkykgqg4znky86nyrtc45q336yv",
    Amount = new List<Coin>(new []{new Coin("uluna", 100000000M)})
};

var fee = new Fee
{
    GasLimit = 46467,
    Amount = new List<Coin>(new []{new Coin("uluna", 698M)})
};

var signDoc = new SignDoc
{
    ChainId = "columbus-3-testnet",
    AccountNumber = 45.ToString(),
    Sequence = 0,
    AuthInfo = new AuthInfo {SignerInfos = new List<SignerInfo>(), Fee = fee},
    TxBody = new TxBody { Messages = new List<Msg>(new []{msgSend})}
};

var single = rk.CreateSignatureAmino(signDoc);
Dump(single.Data.Single.Signature);
