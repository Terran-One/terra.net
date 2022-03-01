using System.Collections.Generic;

namespace Terra.Sdk.Lcd.Models.Entities.Account
{
    public class VestingSchedule
    {
        public string Denom { get; set; }

        public List<VestingScheduleEntry> Schedules { get; set; }
    }
}
