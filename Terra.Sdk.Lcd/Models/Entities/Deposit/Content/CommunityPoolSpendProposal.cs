using System.Collections.Generic;
using ProtoBuf;

namespace Terra.Sdk.Lcd.Models.Entities.Deposit.Content
{
    [ProtoContract]
    public class CommunityPoolSpendProposal : Content
    {
        [ProtoMember(2, Name = "title")] public string Title { get; set; }
        [ProtoMember(3, Name = "description")] public string Description { get; set; }
        [ProtoMember(4, Name = "recipient")] public string Recipient { get; set; }
        [ProtoMember(5, Name = "amount")] public List<Coin> Amount { get; set; }
    }
}
