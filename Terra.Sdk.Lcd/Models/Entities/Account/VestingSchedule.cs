using System.Collections.Generic;
using Newtonsoft.Json;

namespace Terra.Sdk.Lcd.Models.Entities.Account
{
    public class VestingSchedule
    {
        [JsonProperty("denom")]
        public string Denom { get; set; }

        [JsonProperty("schedules")]
        public List<VestingScheduleEntry> Schedules { get; set; }
    }
}