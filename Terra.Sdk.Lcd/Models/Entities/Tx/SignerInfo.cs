using ProtoBuf;
using Terra.Sdk.Lcd.Models.Entities.PubKey;

namespace Terra.Sdk.Lcd.Models.Entities.Tx
{
    [ProtoContract]
    public class SignerInfo
    {
        [ProtoMember(1, Name = "public_key")]
        public PublicKey PublicKey { get; set; }
        [ProtoMember(2, Name = "mode_info")] public ModeInfo ModeInfo { get; set; }
        [ProtoMember(3, Name = "sequence")] public long Sequence { get; set; }
    }
}
