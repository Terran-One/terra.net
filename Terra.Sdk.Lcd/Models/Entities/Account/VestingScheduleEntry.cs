using Newtonsoft.Json;

namespace Terra.Sdk.Lcd.Models.Entities.Account
{
    public class VestingScheduleEntry
    {
        [JsonProperty("start_time")]
        public long StartTime { get; set; }

        [JsonProperty("end_time")]
        public long EndTime { get; set; }

        [JsonProperty("ratio")]
        public decimal Ratio { get; set; }
    }
}