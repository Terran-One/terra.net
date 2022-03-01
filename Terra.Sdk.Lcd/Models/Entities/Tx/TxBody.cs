using System.Collections.Generic;
using Newtonsoft.Json;

namespace Terra.Sdk.Lcd.Models.Entities.Tx
{
    public class TxBody
    {
        [JsonProperty("messages")]
        public List<Msg.Msg> Messages { get; set; }

        [JsonProperty("memo")]
        public string Memo { get; set; }

        [JsonProperty("timeout_height")]
        public long TimeoutHeight { get; set; }
    }
}
