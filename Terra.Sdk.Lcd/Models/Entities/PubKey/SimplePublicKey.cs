using Newtonsoft.Json;

namespace Terra.Sdk.Lcd.Models.Entities.PubKey
{
    public class SimplePublicKey : PubKey
    {
        [JsonProperty("key")]
        public string Key { get; set; }
    }
}