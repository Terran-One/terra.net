using System.Collections.Generic;

namespace Terra.Sdk.Lcd.Models.Entities.Allowance
{
    public class PeriodicAllowance : Allowance
    {
        public BasicAllowance Basic { get; set; }
        public string Period { get; set; }
        public List<Coin> PeriodSpendLimit { get; set; }
        public List<Coin> PeriodCanSpend { get; set; }
        public string PeriodReset { get; set; }
    }
}
