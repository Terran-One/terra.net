using System.Collections.Generic;
using ProtoBuf;

namespace Terra.Sdk.Lcd.Models.Entities.Allowance
{
    [ProtoContract]
    public class AllowedMsgAllowance : Allowance
    {
        [ProtoMember(4, Name = "allowance")] public Allowance Allowance { get; set; }
        [ProtoMember(5, Name = "allowed_messages")] public List<string> AllowedMessages { get; set; }
    }
}
