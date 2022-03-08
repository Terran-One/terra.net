using ProtoBuf;

namespace Terra.Sdk.Lcd.Models.Entities.Tx.Msg.DistributionMsg
{
    [ProtoContract]
    public class MsgSetWithdrawAddress : Msg
    {
        [ProtoMember(1)] public string DelegatorAddress { get; set; }
        [ProtoMember(2)] public string WithdrawAddress { get; set; }
    }
}