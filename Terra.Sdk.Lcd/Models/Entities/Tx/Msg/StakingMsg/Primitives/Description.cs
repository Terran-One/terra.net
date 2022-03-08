using ProtoBuf;

namespace Terra.Sdk.Lcd.Models.Entities.Tx.Msg.StakingMsg.Primitives
{
    [ProtoContract]
    public class Description
    {
        [ProtoMember(1)] public string Moniker { get; set; }
        [ProtoMember(2)] public string Identity { get; set; }
        [ProtoMember(3)] public string Website { get; set; }
        [ProtoMember(4)] public string Details { get; set; }
        [ProtoMember(5)] public string SecurityContact { get; set; }
    }
}