using System.Collections.Generic;
using Newtonsoft.Json;

namespace Terra.Sdk.Lcd.Models.Entities.Tx
{
    public class Tx
    {
        [JsonProperty("body")]
        public TxBody Body { get; set; }

        [JsonProperty("auth_info")]
        public AuthInfo AuthInfo { get; set; }

        [JsonProperty("signatures")]
        public List<string> Signatures { get; set; }
    }
}