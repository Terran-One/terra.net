using System.Collections.Generic;

namespace Terra.Sdk.Lcd.Models.Entities.Tx.Msg.OracleMsg
{
    public class MsgAggregateExchangeRateVote : Msg
    {
        public List<Coin> ExchangeRates { get; set; }
        public string Salt { get; set; }
        public string Validator { get; set; }
    }
}
