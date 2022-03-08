using ProtoBuf;
using Terra.Sdk.Lcd.Models.Entities.PubKey;

namespace Terra.Sdk.Lcd.Models.Entities.Tx
{
    [ProtoContract]
    public class SignerInfo
    {
        [ProtoMember(1)]public PublicKey PublicKey { get; set; }
        [ProtoMember(2)]public long Sequence { get; set; }
        [ProtoMember(3)]public ModeInfo ModeInfo { get; set; }
    }
}
