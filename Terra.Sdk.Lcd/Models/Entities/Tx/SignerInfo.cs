using ProtoBuf;
using Terra.Sdk.Lcd.Extensions;
using Terra.Sdk.Lcd.Models.Entities.PubKey;

namespace Terra.Sdk.Lcd.Models.Entities.Tx
{
    [ProtoContract]
    public class SignerInfo
    {
        [ProtoMember(1, Name = "public_key")]
        public Any ProtoPublicKey
        {
            get => _protoPublicKey;
            set
            {
                _protoPublicKey = value;
                _publicKey = value.Decode<PublicKey>();
            }
        }
        private Any _protoPublicKey;

        public PublicKey PublicKey
        {
            get => _publicKey;
            set
            {
                _publicKey = value;
                ProtoPublicKey = new Any
                {
                    TypeUrl = value.Type,
                    Value = value.EncodeProto()
                };
            }
        }
        private PublicKey _publicKey;

        [ProtoMember(2, Name = "mode_info")]
        public ModeInfo ModeInfo { get; set; }

        [ProtoMember(3, Name = "sequence")]
        public long Sequence { get; set; }
    }
}
