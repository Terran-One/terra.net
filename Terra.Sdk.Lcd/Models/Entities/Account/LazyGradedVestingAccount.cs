using System.Collections.Generic;
using Newtonsoft.Json;

namespace Terra.Sdk.Lcd.Models.Entities.Account
{
    public class LazyGradedVestingAccount : Account
    {
        [JsonProperty("base_vesting_account")]
        public BaseAccount BaseVestingAccount { get; set; }

        [JsonProperty("vesting_schedules")]
        public List<VestingSchedule> VestingSchedules { get; set; }
    }
}