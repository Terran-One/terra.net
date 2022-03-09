using System.Collections.Generic;
using ProtoBuf;

namespace Terra.Sdk.Lcd.Models.Entities.Allowance
{
    [ProtoContract]
    public class PeriodicAllowance : Allowance
    {
        [ProtoMember(4, Name = "basic")] public BasicAllowance Basic { get; set; }
        [ProtoMember(5, Name = "period")] public string Period { get; set; }
        [ProtoMember(6, Name = "period_spend_limit")] public List<Coin> PeriodSpendLimit { get; set; }
        [ProtoMember(7, Name = "period_can_spend")] public List<Coin> PeriodCanSpend { get; set; }
        [ProtoMember(8, Name = "period_reset")] public string PeriodReset { get; set; }
    }
}
