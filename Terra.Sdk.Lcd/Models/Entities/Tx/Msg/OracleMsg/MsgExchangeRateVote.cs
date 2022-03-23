using Newtonsoft.Json;

namespace Terra.Sdk.Lcd.Models.Entities.Tx.Msg.OracleMsg
{
    public class MsgExchangeRateVote : Msg
    {
        protected override System.Type Type => typeof(MsgExchangeRateVote);

        public string Salt { get; set; }
        public string Denom { get; set; }
        public string Feeder { get; set; }
        public string Validator { get; set; }
        [JsonProperty("exchange_rate")]
        public string ExchangeRate { get; set; }
    }
}
