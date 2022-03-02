using System;
using System.Collections.Generic;

namespace Terra.Sdk.Lcd.Models.Entities.Deposit
{
    public class Proposal
    {
        public long Id { get; set; }
        public Content Content { get; set; }
        public ProposalStatus Status { get; set; }
        public FinalTallyResult FinalTallyResult { get; set; }
        public DateTime SubmitTime { get; set; }
        public DateTime DepositEndTime { get; set; }
        public List<Coin> TotalDeposit { get; set; }
        public DateTime VotingStartTime { get; set; }
        public DateTime VotingEndTime { get; set; }
    }

    public class Content
    {
    }
}
