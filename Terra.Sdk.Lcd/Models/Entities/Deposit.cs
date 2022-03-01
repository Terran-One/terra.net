using System.Collections.Generic;

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
        public string ProposalId { get; set; }
        public string Depositor { get; set; }
        public List<Coin> Amount { get; set; }
    }
}
