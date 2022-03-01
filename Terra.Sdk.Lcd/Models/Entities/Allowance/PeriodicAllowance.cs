using System.Collections.Generic;
using Newtonsoft.Json;

namespace Terra.Sdk.Lcd.Models.Entities.Allowance
{
    public class PeriodicAllowance : Allowance
    {
        [JsonProperty("basic")]
        public BasicAllowance Basic { get; set; }

        [JsonProperty("period")]
        public string Period { get; set; }

        [JsonProperty("period_spend_limit")]
        public List<Coin> PeriodSpendLimit { get; set; }

        [JsonProperty("period_can_spend")]
        public List<Coin> PeriodCanSpend { get; set; }

        [JsonProperty("period_reset")]
        public string PeriodReset { get; set; }
    }
}