using System;
using System.Collections.Generic;
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

        public SignatureV2 CreateSignatureAmino(SignDoc signDoc)
        {
            var sigBytes = Convert.ToBase64String(signDoc.ToAminoBytes());

            return new SignatureV2
            {
                PublicKey = _publicKey,
                Data = new ModeInfo
                {
                    Single = new ModeInfo.SingleMode
                    {
                        Mode = ModeInfo.SignMode.Direct,
                        Signature = sigBytes

                    }
                },
                Sequence = signDoc.Sequence
            };
        }

        public async Task<Tx> SignTx(Tx tx, SignOptions options)
        {
            var copyTx = new Tx
            {
                Body = tx.Body,
                AuthInfo = new AuthInfo
                {
                    SignerInfos = new List<SignerInfo>(),
                    Fee = tx.AuthInfo.Fee
                },
                Signatures = new List<string>()
            };

            var signDoc = new SignDoc
            {
                ChainId = options.ChainId,
                AccountNumber = options.AccountNumber.ToString(),
                Sequence = options.Sequence,
                AuthInfo = copyTx.AuthInfo,
                TxBody = copyTx.Body
            };

            SignatureV2 signature;
            if (options.SignMode == ModeInfo.SignMode.LegacyAminoJson)
            {
                signature = CreateSignatureAmino(signDoc);
            }
            else
            {
                signature = await CreateSignature(signDoc);
            }

            var sigData = signature.Data.Single;
            copyTx.Signatures.AddRange(tx.Signatures);
            copyTx.Signatures.Add(sigData.Signature);
            copyTx.AuthInfo.SignerInfos.AddRange(tx.AuthInfo.SignerInfos);
            copyTx.AuthInfo.SignerInfos.Add(new SignerInfo
            {
                PublicKey = signature.PublicKey,
                Sequence = signature.Sequence,
                ModeInfo = new ModeInfo
                {
                    Single = new ModeInfo.SingleMode { Mode = sigData.Mode }
                }
            });

            return copyTx;
        }
    }
}
