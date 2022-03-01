namespace Terra.Sdk.Lcd.Models.Entities.Tx.Msg.DistributionMsg
{
    public class MsgWithdrawDelegatorReward : Msg
    {
        public string DelegatorAddress { get; set; }
        public string ValidatorAddress { get; set; }
    }
}
