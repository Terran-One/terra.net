using ProtoBuf;

namespace Terra.Sdk.Lcd.Models.Entities.Tx.Msg.StakingMsg
{
    [ProtoContract]
    public class MsgBeginRedelegate : Msg
    {
        [ProtoMember(1)] public string DelegatorAddress { get; set; }
        [ProtoMember(2)] public string ValidatorSrcAddress { get; set; }
        [ProtoMember(3)] public string ValidatorDstAddress { get; set; }
        [ProtoMember(4)] public Coin Amount { get; set; }
    }
}