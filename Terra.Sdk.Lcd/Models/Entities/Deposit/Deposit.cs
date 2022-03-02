using System.Collections.Generic;

namespace Terra.Sdk.Lcd.Models.Entities.Deposit
{
    public class Deposit
    {
        public string ProposalId { get; set; }
        public string Depositor { get; set; }
        public List<Coin> Amount { get; set; }
    }
}