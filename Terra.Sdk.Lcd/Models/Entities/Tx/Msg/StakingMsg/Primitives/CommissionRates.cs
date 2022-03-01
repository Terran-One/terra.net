namespace Terra.Sdk.Lcd.Models.Entities.Tx.Msg.StakingMsg.Primitives
{
    public class CommissionRates
    {
        public decimal Rate { get; set; }
        public decimal MaxRate { get; set; }
        public decimal  MaxChangeRate { get; set; }
    }
}
