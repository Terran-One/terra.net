using System.Collections.Generic;
using Newtonsoft.Json;

namespace Terra.Sdk.Lcd.Models.Entities
{
    public class Proposal
    {
        
    }

    public class Vote
    {
        
    }
    public class Tally
    {
        
    }
    public class DepositParams
    {
        
    }
    public class VotingParams
    {
        
    }
    public class TallyParams
    {
        
    }
    public class GovParams
    {
        
    }

    public class Deposit
    {
        [JsonProperty("proposal_id")]
        public string ProposalId { get; set; }

        [JsonProperty("depositor")]
        public string Depositor { get; set; }

        [JsonProperty("amount")]
        public List<Coin> Amount { get; set; }
    }
}