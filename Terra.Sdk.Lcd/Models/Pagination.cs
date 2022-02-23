using Newtonsoft.Json;

namespace Terra.Sdk.Lcd.Models
{
    public class Pagination
    {
        [JsonProperty("next_key")]
        public string NextKey { get; set; }

        [JsonProperty("total")]
        public int? TotalCount { get; set; }
    }
}
