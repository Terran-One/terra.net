using ProtoBuf;

namespace Terra.Sdk.Lcd.Models.Entities.Deposit.Content
{
    [ProtoContract]public class TextProposal : Content
    {
        [ProtoMember(2)]public string Title { get; set; }
        [ProtoMember(3)]public string Description { get; set; }
    }
}
