using System;
using ProtoBuf;

namespace Terra.Sdk.Lcd.Models.Entities.Deposit.Content
{
    [ProtoContract]
    public class TextProposal : Content
    {
        protected override Type Type => typeof(TextProposal);

        [ProtoMember(2, Name = "title")] public string Title { get; set; }
        [ProtoMember(3, Name = "description")] public string Description { get; set; }
    }
}
