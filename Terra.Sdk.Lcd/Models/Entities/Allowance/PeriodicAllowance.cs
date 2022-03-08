using System.Collections.Generic;
using ProtoBuf;

namespace Terra.Sdk.Lcd.Models.Entities.Allowance
{
    [ProtoContract]
    public class PeriodicAllowance : Allowance
    {
        [ProtoMember(4)] public BasicAllowance Basic { get; set; }
        [ProtoMember(5)] public string Period { get; set; }
        [ProtoMember(6)] public List<Coin> PeriodSpendLimit { get; set; }
        [ProtoMember(7)] public List<Coin> PeriodCanSpend { get; set; }
        [ProtoMember(8)] public string PeriodReset { get; set; }
    }
}