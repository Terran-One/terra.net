using Terra.Sdk.Lcd.Models.Entities.Tx.Msg.StakingMsg.Primitives;
using ProtoBuf;

namespace Terra.Sdk.Lcd.Models.Entities.Tx.Msg.StakingMsg
{
    [ProtoContract]
    public class MsgEditValidator : Msg
    {
        [ProtoMember(1, Name = "description")] public Description Description { get; set; }
        [ProtoMember(2, Name = "validator_address")] public string ValidatorAddress { get; set; }
        [ProtoMember(3, Name = "commission_rate")] public decimal CommissionRate { get; set; }
        [ProtoMember(4, Name = "min_self_delegation")] public int MinSelfDelegation { get; set; }
    }
}
