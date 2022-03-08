using ProtoBuf; namespace Terra.Sdk.Lcd.Models.Entities.Tx.Msg.StakingMsg
{
    [ProtoContract]public class MsgDelegate : Msg
    {
        [ProtoMember(1)]public string DelegatorAddress { get; set; }
        [ProtoMember(2)]public string ValidatorAddress { get; set; }
        [ProtoMember(3)]public Coin Amount { get; set; }
    }
}
