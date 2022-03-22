using System;
using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;
using dotnetstandard_bip32;
using Nethereum.Util;
using Terra.Sdk.Lcd.Extensions;

namespace Terra.Sdk.Lcd.Models.Signing
{
    internal readonly struct Bip32
    {
        public static Bip32 FromSeed(string seed)
        {
            if (seed.Length < 32)
                throw new ArgumentException("Seed should be at least 128 bits");

            if (seed.Length > 128)
                throw new ArgumentException("Seed should be at most 512 bits");

            byte[] i;
            using (var hmacSha512 = new HMACSHA512(Encoding.UTF8.GetBytes("Bitcoin seed")))
                i = hmacSha512.ComputeHash(seed.HexStringToByteArray());

            var il = i.Slice(0, 32);
            if (!IsPrivate(il))
                throw new ArgumentException("Private key not in range [1, n)");

            var ir = i.Slice(32);
            return new Bip32(il, ir, ir);
        }

        private Bip32(byte[] privateKey, byte[] publicKey, byte[] chainCode)
        {
            PrivateKey = privateKey;
            PublicKey = publicKey;
            ChainCode = chainCode;
        }

        public byte[] PrivateKey { get; }
        public byte[] PublicKey { get; }
        public byte[] ChainCode { get; }

        public Bip32 DerivePath(string path) => path.Split('/').Slice(1).Aggregate(
            this,
            (prevHd, indexStr) => indexStr.EndsWith("'")
                ? prevHd.Derive(Convert.ToUInt32(indexStr.Substring(0, indexStr.Length - 1), 10) + HighestBit)
                : prevHd.Derive(Convert.ToUInt32(indexStr, 10)));

        private const uint HighestBit = 0x80000000; // 2147483648

        private Bip32 Derive(uint index)
        {
            var isHardened = index >= HighestBit;
            var data = new byte[37];

            if (isHardened)
            {
                data[0] = 0x00;
                PrivateKey.CopyTo(data, 1);
                BitConverter.GetBytes(index).Reverse().ToArray().CopyTo(data, 33);
            }
            else
            {
                PublicKey.CopyTo(data, 0);
                BitConverter.GetBytes(index).Reverse().ToArray().CopyTo(data, 33);
            }

            byte[] I;
            using (var hmacSha512 = new HMACSHA512(ChainCode))
                I = hmacSha512.ComputeHash(data);

            var il = I.Slice(0, 32);
            var ir = I.Slice(32);

            if (!IsPrivate(il))
                return Derive(index + 1);

            PrivateKeyTweakAdd(PrivateKey, il);
            var ki = PrivateKey;
            // In case ki == 0, proceed with the next value for i
            if (ki == null)
                return Derive(index + 1);

            return new Bip32(ki, Array.Empty<byte>(), ir);
        }

        private const int PrivateKeySize = 32;
        private static readonly byte[] Bn32Zero = new byte[32];
        private static readonly byte[] Bn32N =
        {
            255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255,
            254, 186, 174, 220, 230, 175, 72, 160, 59, 191, 210, 94, 140, 208, 54, 65, 65,
        };

        private static bool IsPrivate(byte[] x) =>
            x.Length == PrivateKeySize &&
            CmpBn32(x, Bn32Zero) > 0 &&
            CmpBn32(x, Bn32N) < 0;

        private static int CmpBn32(byte [] data1, byte[] data2)
        {
            for (var i = 0; i < 32; ++i)
            {
                if (data1[i] != data2[i])
                {
                    return data1[i] < data2[i] ? -1 : 1;
                }
            }

            return 0;
        }

        private static int PrivateKeyTweakAdd(byte[] seckey, byte[] tweak)
        {
            var n = BigInteger.Parse("fffffffffffffffffffffffffffffffebaaedce6af48a03bbfd25e8cd0364141", NumberStyles.HexNumber);

            var bn = new BigInteger(tweak);
            if (bn.CompareTo(n) >= 0)
                return 1;

            bn = BigInteger.Add(bn, new BigInteger(seckey));
            if (bn.CompareTo(n) >= 0)
                bn = BigInteger.Subtract(bn, n);

            if (bn.IsZero)
                return 1;

            var tweaked = bn.ToByteArray().Reverse().Take(32).ToArray(); // Uint8Array, 'be', 32
            tweaked.CopyTo(seckey, 0);

            return 0;
        }
    }
}
