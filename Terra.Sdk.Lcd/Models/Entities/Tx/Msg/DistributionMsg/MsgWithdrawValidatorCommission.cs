using ProtoBuf;

namespace Terra.Sdk.Lcd.Models.Entities.Tx.Msg.DistributionMsg
{
    [ProtoContract]
    public class MsgWithdrawValidatorCommission : Msg
    {
        [ProtoMember(1, Name = "validator_address")]
        public string ValidatorAddress { get; set; }
    }
}
