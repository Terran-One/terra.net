using ProtoBuf;

namespace Terra.Sdk.Lcd.Models.Entities.Tx.Msg.DistributionMsg
{
    [ProtoContract]
    public class MsgWithdrawDelegatorReward : Msg
    {
        [ProtoMember(1, Name = "delegator_address")]
        public string DelegatorAddress { get; set; }

        [ProtoMember(2, Name = "validator_address")]
        public string ValidatorAddress { get; set; }
    }
}
