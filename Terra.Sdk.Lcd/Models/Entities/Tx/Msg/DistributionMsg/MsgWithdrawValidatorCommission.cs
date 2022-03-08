using ProtoBuf;

namespace Terra.Sdk.Lcd.Models.Entities.Tx.Msg.DistributionMsg
{
    [ProtoContract]
    public class MsgWithdrawValidatorCommission : Msg
    {
        [ProtoMember(1)] public string ValidatorAddress { get; set; }
    }
}