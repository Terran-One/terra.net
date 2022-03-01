namespace Terra.Sdk.Lcd.Models.Entities.Account
{
    public class VestingScheduleEntry
    {
        public long StartTime { get; set; }
        public long EndTime { get; set; }
        public decimal Ratio { get; set; }
    }
}
