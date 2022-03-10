using System;
using ProtoBuf;

namespace Terra.Sdk.Lcd.Models.Entities.Deposit.Content
{
    [ProtoContract]
    public class SoftwareUpgradeProposal : Content
    {
        protected override Type Type => typeof(SoftwareUpgradeProposal);

        [ProtoMember(2, Name = "title")] public string Title { get; set; }
        [ProtoMember(3, Name = "description")] public string Description { get; set; }
        [ProtoMember(4, Name = "plan")] public Plan Plan { get; set; }
    }
}
