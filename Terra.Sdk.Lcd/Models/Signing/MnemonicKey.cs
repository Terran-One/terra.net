using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using dotnetstandard_bip32;

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
            var masterKey = GetMasterKeyFromSeed(seed);
            var hdPathLuna = $"m/44'/{options.CoinType}'/{options.Account}'/0/{options.Index}";
            var terraHd = DerivePath(masterKey, hdPathLuna);
            Console.WriteLine(Convert.ToBase64String(terraHd.Key));
            return terraHd.Key;
        }

        private const string Curve = "Bitcoin seed";
        private const uint HardenedOffset = 2147483648;

        private static (byte[] Key, byte[] ChainCode) DerivePath((byte[] Key, byte[] ChainCode) masterKey, string path)
        {
            // var splitPath = path.Split('/').Slice(1);
            // return splitPath.Aggregate((prevHd, indexStr) =>
            // {
            //     uint index;
            //     if (indexStr.EndsWith("'"))
            //     {
            //         index = Convert.ToUInt32(indexStr.Substring(0, indexStr.Length - 1), 10);
            //         return prevHd.deriveHardened(index);
            //     }
            //
            //     index = Convert.ToUInt32(indexStr, 10);
            //     return prevHd.derive(index);
            // });
            var pathBytes = path.Split('/').Slice(1);
            return pathBytes.Aggregate(masterKey, (mks, next) =>
                GetChildKeyDerivation(mks.Item1, mks.Item2, next.EndsWith("'")
                    ? Convert.ToUInt32(next.Replace("'", ""), 10) + HardenedOffset
                    : Convert.ToUInt32(next, 10)));
        }

        private static (byte[] Key, byte[] ChainCode) GetMasterKeyFromSeed(string seed)
        {
            using (var hmacshA512 = new HMACSHA512(Encoding.UTF8.GetBytes(Curve)))
            {
                var hash = hmacshA512.ComputeHash(seed.HexToByteArray());
                return (hash.Slice(0, 32), hash.Slice(32));
            }
        }

        private static (byte[] Key, byte[] ChainCode) GetChildKeyDerivation(byte[] key, byte[] chainCode, uint index)
        {
            var bigEndianBuffer = new BigEndianBuffer();
            bigEndianBuffer.Write(new byte[1]);
            bigEndianBuffer.Write(key);
            bigEndianBuffer.WriteUInt(index);
            using (var hmacshA512 = new HMACSHA512(chainCode))
            {
                var hash = hmacshA512.ComputeHash(bigEndianBuffer.ToArray());
                return (hash.Slice(0, 32), hash.Slice(32));
            }
        }
    }
}
