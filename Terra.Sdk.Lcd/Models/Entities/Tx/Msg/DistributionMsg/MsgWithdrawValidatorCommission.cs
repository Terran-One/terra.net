using ProtoBuf;

namespace Terra.Sdk.Lcd.Models.Entities.Tx.Msg.DistributionMsg
{
    [ProtoContract]
    public class MsgWithdrawValidatorCommission : Msg
    {
        protected override System.Type Type => typeof(MsgWithdrawValidatorCommission);

        [ProtoMember(1, Name = "validator_address")]
        public string ValidatorAddress { get; set; }
    }
}
