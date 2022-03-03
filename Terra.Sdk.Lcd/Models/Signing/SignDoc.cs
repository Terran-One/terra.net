using System.IO;
using ProtoBuf;
using Terra.Sdk.Lcd.Models.Entities.Tx;

namespace Terra.Sdk.Lcd.Models.Signing
{
    public class SignDoc
    {
        public string ChainId { get; set; }
        public string AccountNumber { get; set; }
        public long Sequence { get; set; }
        public AuthInfo AuthInfo { get; set; }
        public TxBody TxBody { get; set; }

        public byte[] ToBytes()
        {
            return ToProto(new
            {
                BodyBytes = TxBody,
                AuthInfoBytes = AuthInfo,
                ChainId,
                AccountNumber = long.Parse(AccountNumber)
            });
        }

        internal byte[] ToAminoBytes() => ToProto(new
        {
            ChainId,
            AccountNumber,
            Sequence = Sequence.ToString(),
            TimeoutHeight =
                TxBody.TimeoutHeight != 0
                    ? TxBody.TimeoutHeight.ToString()
                    : null,
            Fee = AuthInfo.Fee.ToAmino(),
            Msgs = TxBody.Messages,
            Memo = TxBody.Memo ?? "",
        });

        private static byte[] ToProto(object obj)
        {
            using (var stream = new MemoryStream())
            {
                Serializer.Serialize(stream, obj);
                return stream.ToArray();
            }
        }
    }
}
