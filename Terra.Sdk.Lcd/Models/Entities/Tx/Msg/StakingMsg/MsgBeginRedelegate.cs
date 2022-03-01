namespace Terra.Sdk.Lcd.Models.Entities.Tx.Msg.StakingMsg
{
    public class MsgBeginRedelegate : Msg
    {
        public string DelegatorAddress { get; set; }
        public string ValidatorSrcAddress { get; set; }
        public string ValidatorDstAddress { get; set; }
        public Coin Amount { get; set; }
    }
}
