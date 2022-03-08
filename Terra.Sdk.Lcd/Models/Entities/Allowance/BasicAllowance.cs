using System.Collections.Generic;
using ProtoBuf;

namespace Terra.Sdk.Lcd.Models.Entities.Allowance
{
    [ProtoContract]
    public class BasicAllowance : Allowance
    {
        [ProtoMember(4)] public List<Coin> SpendLimit { get; set; }
        [ProtoMember(5)] public string Expiration { get; set; }
    }
}