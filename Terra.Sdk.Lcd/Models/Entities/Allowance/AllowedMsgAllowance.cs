using System.Collections.Generic;
using Newtonsoft.Json;

namespace Terra.Sdk.Lcd.Models.Entities.Allowance
{
    public class AllowedMsgAllowance : Allowance
    {
        [JsonProperty("allowance")]
        public Allowance Allowance { get; set; }

        [JsonProperty("allowed_messages")]
        public List<string> AllowedMessages { get; set; }
    }
}