namespace Terra.Sdk.Lcd.Models.Entities.Tx.Msg.MarketMsg
{
    public class MsgSwapSend : Msg
    {
        public string FromAddress { get; set; }
        public string ToAddress { get; set; }
        public Coin OfferCoin { get; set; }
        public string AskDenom { get; set; }
    }
}
