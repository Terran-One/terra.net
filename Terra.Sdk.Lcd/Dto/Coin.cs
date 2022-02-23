using Newtonsoft.Json;

namespace Terra.Sdk.Lcd.Dto
{
    public class Coin
    {
        [JsonProperty("denom")]
        public string Denom { get; set; }

        [JsonProperty("amount")]
        public decimal Amount { get; set; }
    }
}