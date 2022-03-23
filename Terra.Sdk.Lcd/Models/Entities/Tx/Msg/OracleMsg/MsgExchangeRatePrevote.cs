namespace Terra.Sdk.Lcd.Models.Entities.Tx.Msg.OracleMsg
{
    public class MsgExchangeRatePrevote : Msg
    {
        protected override System.Type Type => typeof(MsgExchangeRatePrevote);

        public string Hash { get; set; }
        public string Denom { get; set; }
        public string Feeder { get; set; }
        public string Validator { get; set; }
    }
}