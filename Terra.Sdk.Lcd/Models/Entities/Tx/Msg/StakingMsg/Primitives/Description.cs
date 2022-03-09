using ProtoBuf;

namespace Terra.Sdk.Lcd.Models.Entities.Tx.Msg.StakingMsg.Primitives
{
    [ProtoContract]
    public class Description
    {
        [ProtoMember(1, Name = "moniker")] public string Moniker { get; set; }
        [ProtoMember(2, Name = "identity")] public string Identity { get; set; }
        [ProtoMember(3, Name = "website")] public string Website { get; set; }
        [ProtoMember(4, Name = "details")] public string Details { get; set; }
        [ProtoMember(5, Name = "security_contract")] public string SecurityContact { get; set; }
    }
}
