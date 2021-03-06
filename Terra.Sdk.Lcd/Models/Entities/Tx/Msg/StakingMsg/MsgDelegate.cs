using ProtoBuf;

namespace Terra.Sdk.Lcd.Models.Entities.Tx.Msg.StakingMsg
{
    [ProtoContract]
    public class MsgDelegate : Msg
    {
        protected override System.Type Type => typeof(MsgDelegate);

        [ProtoMember(1, Name = "delegator_address")] public string DelegatorAddress { get; set; }
        [ProtoMember(2, Name = "validator_address")] public string ValidatorAddress { get; set; }
        [ProtoMember(3, Name = "amount")] public Coin Amount { get; set; }
    }
}
