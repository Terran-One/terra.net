using ProtoBuf;

namespace Terra.Sdk.Lcd.Models.Entities.Tx.Msg.StakingMsg
{
    [ProtoContract]
    public class MsgBeginRedelegate : Msg
    {
        protected override System.Type Type => typeof(MsgBeginRedelegate);

        [ProtoMember(1, Name = "delegator_address")] public string DelegatorAddress { get; set; }
        [ProtoMember(2, Name = "validator_src_address")] public string ValidatorSrcAddress { get; set; }
        [ProtoMember(3, Name = "validator_dst_address")] public string ValidatorDstAddress { get; set; }
        [ProtoMember(4, Name = "amount")] public Coin Amount { get; set; }
    }
}
