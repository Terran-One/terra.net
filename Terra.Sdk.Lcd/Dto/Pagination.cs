using Newtonsoft.Json;

namespace Terra.Sdk.Lcd.Dto
{
    public class Pagination
    {
        [JsonProperty("next_key")]
        public string NextKey { get; set; }

        [JsonProperty("total")]
        public int Total { get; set; }
    }
}