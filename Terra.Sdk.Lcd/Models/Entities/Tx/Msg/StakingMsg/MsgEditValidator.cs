using Terra.Sdk.Lcd.Models.Entities.Tx.Msg.StakingMsg.Primitives;

using ProtoBuf; namespace Terra.Sdk.Lcd.Models.Entities.Tx.Msg.StakingMsg
{
    [ProtoContract]public class MsgEditValidator : Msg
    {
        [ProtoMember(1)]public Description Description { get; set; }
        [ProtoMember(2)]public string ValidatorAddress { get; set; }
        [ProtoMember(3)]public decimal CommissionRate { get; set; }
        [ProtoMember(4)]public int MinSelfDelegation { get; set; }
    }
}
