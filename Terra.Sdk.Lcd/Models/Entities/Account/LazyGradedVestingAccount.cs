using System.Collections.Generic;

namespace Terra.Sdk.Lcd.Models.Entities.Account
{
    public class LazyGradedVestingAccount : Account
    {
        public BaseAccount BaseVestingAccount { get; set; }

        public List<VestingSchedule> VestingSchedules { get; set; }
    }
}
