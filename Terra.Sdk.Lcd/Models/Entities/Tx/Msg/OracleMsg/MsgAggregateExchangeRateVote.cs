using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using ProtoBuf;

namespace Terra.Sdk.Lcd.Models.Entities.Tx.Msg.OracleMsg
{
    [ProtoContract]
    public class MsgAggregateExchangeRateVote : Msg
    {
        protected override System.Type Type => typeof(MsgAggregateExchangeRateVote);

        [ProtoMember(1, Name = "exchange_rates")]
        [JsonProperty("exchange_rates")]
        public string SerializedExchangeRates { get; set; }

        [JsonIgnore]
        public IReadOnlyCollection<Coin> ExchangeRates => SerializedExchangeRates.Split(',').Select(s =>
        {
            var length = s.Length - 4;
            var value = decimal.Parse(s.Substring(0, length));
            var denom = s.Substring(length);
            return new Coin(denom, value);
        }).ToArray();

        [ProtoMember(2, Name = "salt")] public string Salt { get; set; }
        [ProtoMember(3, Name = "validator")] public string Validator { get; set; }
    }
}
