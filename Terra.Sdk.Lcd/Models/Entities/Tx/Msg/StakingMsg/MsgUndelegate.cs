namespace Terra.Sdk.Lcd.Models.Entities.Tx.Msg.StakingMsg
{
    public class MsgUndelegate : Msg
    {
        public string DelegatorAddress { get; set; }
        public string ValidatorAddress { get; set; }
        public Coin Amount { get; set; }
    }
}
