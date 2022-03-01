namespace Terra.Sdk.Lcd.Models.Entities.Tx.Msg.MarketMsg
{
    public class MsgSwap : Msg
    {
        public string Trader { get; set; }
        public Coin OfferCoin { get; set; }
        public string AskDenom { get; set; }
    }
}
