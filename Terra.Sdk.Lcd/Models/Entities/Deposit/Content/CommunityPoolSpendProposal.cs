using System.Collections.Generic;

namespace Terra.Sdk.Lcd.Models.Entities.Deposit.Content
{
    public class CommunityPoolSpendProposal : Content
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Recipient { get; set; }
        public List<Coin> Amount { get; set; }
    }
}