using ProtoBuf;

namespace Terra.Sdk.Lcd.Models.Entities.Tx.Msg.DistributionMsg
{
    [ProtoContract]
    public class MsgSetWithdrawAddress : Msg
    {
        protected override System.Type Type => typeof(MsgSetWithdrawAddress);

        [ProtoMember(1, Name = "delegator_address")] public string DelegatorAddress { get; set; }
        [ProtoMember(2, Name = "withdraw_address")] public string WithdrawAddress { get; set; }
    }
}
