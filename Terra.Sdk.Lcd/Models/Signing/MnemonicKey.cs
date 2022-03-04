using System;
using System.Threading.Tasks;

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
            var seed = MnemonicKeyOptions.Bip39.MnemonicToSeedHex(options.Mnemonic, null);
            var hdPathLuna = $"m/44'/{options.CoinType}'/{options.Account}'/0/{options.Index}";
            var terraHd = MnemonicKeyOptions.Bip32.DerivePath(hdPathLuna, seed);
            return terraHd.Key;
        }

        public override Task<byte[]> Sign(byte[] payload)
        {
            throw new NotImplementedException();
        }
    }
}
