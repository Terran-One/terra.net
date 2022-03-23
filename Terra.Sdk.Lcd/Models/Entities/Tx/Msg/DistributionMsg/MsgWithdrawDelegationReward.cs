namespace Terra.Sdk.Lcd.Models.Entities.Tx.Msg.DistributionMsg
{
    public class MsgWithdrawDelegationReward : Msg
    {
        protected override System.Type Type => typeof(MsgWithdrawDelegationReward);

        public string DelegatorAddress { get; set; }
        public string ValidatorAddress { get; set; }
    }
}
