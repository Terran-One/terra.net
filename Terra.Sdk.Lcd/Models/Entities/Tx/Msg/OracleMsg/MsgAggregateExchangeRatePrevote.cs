namespace Terra.Sdk.Lcd.Models.Entities.Tx.Msg.OracleMsg
{
    public class MsgAggregateExchangeRatePrevote : Msg
    {
        public string Hash { get; set; }
        public string Feeder { get; set; }
        public string Validator { get; set; }
    }
}
