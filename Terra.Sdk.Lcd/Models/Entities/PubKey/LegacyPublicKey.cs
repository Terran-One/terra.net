using System.Collections.Generic;
using Newtonsoft.Json;

namespace Terra.Sdk.Lcd.Models.Entities.PubKey
{
    public class LegacyPublicKey : PubKey
    {
        [JsonProperty("threshold")]
        public string Threshold { get; set; }

        [JsonProperty("public_keys")]
        public List<SimplePublicKey> PublicKeys { get; set; }
    }
}