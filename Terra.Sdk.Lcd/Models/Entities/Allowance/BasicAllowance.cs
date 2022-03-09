using System.Collections.Generic;
using ProtoBuf;

namespace Terra.Sdk.Lcd.Models.Entities.Allowance
{
    [ProtoContract]
    public class BasicAllowance : Allowance
    {
        [ProtoMember(4, Name = "spend_limit")] public List<Coin> SpendLimit { get; set; }
        [ProtoMember(5, Name = "expiration")] public string Expiration { get; set; }
    }
}
