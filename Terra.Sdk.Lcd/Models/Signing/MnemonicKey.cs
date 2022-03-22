using System;
using Newtonsoft.Json;

namespace Terra.Sdk.Lcd.Models.Signing
{
    public class MnemonicKey : RawKey
    {
        public string Mnemonic { get; set; }

        public MnemonicKey(MnemonicKeyOptions options) : base(CreatePrivateKey(options))
        {
            Mnemonic = options.Mnemonic;
        }

        private static byte[] CreatePrivateKey(MnemonicKeyOptions options)
        {
            var seed = MnemonicKeyOptions.Bip39.MnemonicToSeedHex(options.Mnemonic, "");
            var hdPathLuna = $"m/44'/{options.CoinType}'/{options.Account}'/0/{options.Index}";
            var masterKey = Bip32.FromSeed(seed);
            Console.WriteLine($"*** master key ***\n{JsonConvert.SerializeObject(masterKey, Formatting.Indented)}");
            var terraHd = masterKey.DerivePath(hdPathLuna);
            return terraHd.PrivateKey;
        }
    }
}
