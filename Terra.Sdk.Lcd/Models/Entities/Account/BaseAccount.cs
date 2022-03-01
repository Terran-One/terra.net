using Newtonsoft.Json;

namespace Terra.Sdk.Lcd.Models.Entities.Account
{
    public class BaseAccount : Account
    {
        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("pub_key")]
        public PubKey.PubKey PubKey { get; set; }

        [JsonProperty("account_number")]
        public long AccountNumber { get; set; }

        [JsonProperty("sequence")]
        public long Sequence { get; set; }
    }
}