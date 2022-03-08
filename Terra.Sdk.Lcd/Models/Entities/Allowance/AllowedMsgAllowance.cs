using System.Collections.Generic;
using ProtoBuf;

namespace Terra.Sdk.Lcd.Models.Entities.Allowance
{
    [ProtoContract]
    public class AllowedMsgAllowance : Allowance
    {
        [ProtoMember(4)] public Allowance Allowance { get; set; }
        [ProtoMember(5)] public List<string> AllowedMessages { get; set; }
    }
}
