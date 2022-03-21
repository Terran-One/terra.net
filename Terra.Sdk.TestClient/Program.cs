using Terra.Sdk.Lcd.Models.Signing;
using Terra.Sdk.TestClient;

var mk = new MnemonicKey(new MnemonicKeyOptions
{
    Mnemonic = "wonder caution square unveil april art add hover spend smile proud admit modify old copper throw crew happy nature luggage reopen exhibit ordinary napkin"
});

mk.Dump();

Console.WriteLine($"PrivateKey: {Convert.ToBase64String(mk.PrivateKey)}");
Console.WriteLine("PublicKey:");
mk.PublicKey.Dump();
