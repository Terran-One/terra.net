using dotnetstandard_bip32;
using dotnetstandard_bip39;

namespace Terra.Sdk.Lcd.Models.Signing
{
    public class MnemonicKeyOptions
    {
        public static readonly BIP39 Bip39 = new BIP39();
        public static readonly BIP32 Bip32 = new BIP32();

        public string Mnemonic { get; set; } = Bip39.GenerateMnemonic(256, BIP39Wordlist.English);
        public long Account { get; set; }
        public long Index { get; set; }
        public long CoinType { get; set; } = 330;
    }
}