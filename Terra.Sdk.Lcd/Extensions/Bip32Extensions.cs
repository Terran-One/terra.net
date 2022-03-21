using System;
using System.Linq;
using System.Security.Cryptography;
using dotnetstandard_bip32;

namespace Terra.Sdk.Lcd.Extensions
{
    public static class Bip32Extensions
    {
        public static (byte[] Key, byte[] ChainCode) CustomDerivePath(this BIP32 bip32, string path, string seed)
        {
            (byte[], byte[]) masterKeyFromSeed = bip32.GetMasterKeyFromSeed(seed);
            return path.Split('/').Slice(1)
                .Select(a => (
                    Value: Convert.ToUInt32(a.Replace("'", ""), 10),
                    IsHardened: a.EndsWith("'")
                ))
                .Aggregate(masterKeyFromSeed, (mks, next) => GetChildKeyDerivation(mks.Item1, mks.Item2, next.Item1 + (next.Item2 ? HardenedOffset : 0)));
        }

        private const uint HardenedOffset = 2147483648;

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
