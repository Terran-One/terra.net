using System.Collections.Generic;
using Newtonsoft.Json;

namespace Terra.Sdk.Lcd.Models.Entities.Allowance
{
    public class BasicAllowance : Allowance
    {
        [JsonProperty("spend_limit")]
        public List<Coin> SpendLimit { get; set; }

        [JsonProperty("expiration")]
        public string Expiration { get; set; }
    }
}