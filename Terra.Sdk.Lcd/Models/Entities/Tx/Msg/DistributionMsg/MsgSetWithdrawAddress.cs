namespace Terra.Sdk.Lcd.Models.Entities.Tx.Msg.DistributionMsg
{
    public class MsgSetWithdrawAddress : Msg
    {
        public string DelegatorAddress { get; set; }
        public string WithdrawAddress { get; set; }
    }
}
