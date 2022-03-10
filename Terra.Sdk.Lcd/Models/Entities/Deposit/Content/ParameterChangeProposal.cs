using System;
using System.Collections.Generic;
using ProtoBuf;

namespace Terra.Sdk.Lcd.Models.Entities.Deposit.Content
{
    [ProtoContract]
    public class ParameterChangeProposal : Content
    {
        protected override Type Type => typeof(ParameterChangeProposal);

        [ProtoMember(2, Name = "title")] public string Title { get; set; }
        [ProtoMember(3, Name = "description")] public string Description { get; set; }
        [ProtoMember(4, Name = "changes")] public List<ParamChange> Changes { get; set; }
    }
}
