using System;
using System.Linq;
using System.Threading.Tasks;
using Terra.Sdk.Lcd.Extensions;
using Terra.Sdk.Lcd.Models.Entities.PubKey;
using Terra.Sdk.Lcd.Models.Entities.Tx;

namespace Terra.Sdk.Lcd.Models.Signing
{
    public abstract class Key
    {
        private readonly PublicKey _publicKey;

        protected Key(PublicKey publicKey)
        {
            _publicKey = publicKey;
        }

        public abstract Task<byte[]> Sign(byte[] payload);

        public string AccAddress => _publicKey.Address;
        public string ValAddress => _publicKey.RawAddress.ToHexString().ConvertToBech32AddressFromHex("terravaloper");

        public async Task<SignatureV2> CreateSignature(SignDoc signDoc)
        {
            // backup for restore
            var signerInfos = signDoc.AuthInfo.SignerInfos;
            signDoc.AuthInfo.SignerInfos = new[]
            {
                new SignerInfo
                {
                    PublicKey = _publicKey,
                    Sequence = signDoc.Sequence,
                    ModeInfo = new ModeInfo
                    {
                        Single = new ModeInfo.SingleMode { Mode = ModeInfo.SignMode.Direct }
                    }
                }
            }.ToList();

            var sigBytes = Convert.ToBase64String(await Sign(signDoc.ToBytes()));

            // restore signDoc to origin
            signDoc.AuthInfo.SignerInfos = signerInfos;

            return new SignatureV2
            {
                PublicKey = _publicKey,
                Data = new ModeInfo {Single = new ModeInfo.SingleMode {Mode = ModeInfo.SignMode.Direct, Signature = sigBytes}},
                Sequence = signDoc.Sequence
            };
        }
    }

    public class SignatureV2
    {
        public PublicKey PublicKey { get; set; }
        public ModeInfo Data { get; set; }
        public long Sequence { get; set; }
    }
}
