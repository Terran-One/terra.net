using System;
using System.Threading.Tasks;
using Nethereum.Signer;
using Newtonsoft.Json;
using Org.BouncyCastle.Asn1.Sec;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Utilities.Encoders;
using Terra.Sdk.Lcd.Extensions;
using Terra.Sdk.Lcd.Models.Entities.PubKey;

namespace Terra.Sdk.Lcd.Models.Signing
{
    public class RawKey : Key
    {
        [JsonIgnore]
        public byte[] PrivateKey { get; }

        public RawKey(byte[] privateKey) : base(CreatePublicKey(privateKey))
        {
            PrivateKey = privateKey;
        }

        private static SimplePublicKey CreatePublicKey(byte[] privateKey)
        {
            var publicKey = GetPublicKey(privateKey); //new ECPublicKey(ECCurve.Secp256k1.CreatePoint(new BigInteger(privateKey), true), ECCurve.Secp256k1);
            Console.WriteLine($"pubKey: {string.Join(" ", publicKey)}");
            return new SimplePublicKey
            {
                Key = Convert.ToBase64String(publicKey.HexStringToByteArray())
            };
        }

        public Tuple<byte[], long> EcdsaSign(byte[] payload)
        {
            var key = new EthECKey(PrivateKey, true);
            var sig = key.SignAndCalculateV(payload);
            return Tuple.Create(sig.To64ByteArray(), (long) sig.V[0]);
        }

        public override Task<byte[]> Sign(byte[] payload)
        {
            return Task.FromResult(EcdsaSign(payload).Item1);
        }

        private static string GetPublicKey(byte[] privateKey)
        {
            var privKeyInt = new BigInteger(+1, privateKey);

            var parameters = SecNamedCurves.GetByName("secp256k1");
            var qa = parameters.G.Multiply(privKeyInt).Normalize();

            var bytes = qa.GetEncoded();
            var str = Hex.ToHexString(bytes);
            return str;
        }
    }
}
