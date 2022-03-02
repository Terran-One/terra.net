using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Terra.Sdk.Lcd.Extensions;

namespace Terra.Sdk.Lcd.Models.Entities.Deposit
{
    public class Proposal
    {
        private readonly LcdClient _client;

        /// <remarks>
        /// For serialization.
        /// </remarks>
        public Proposal()
        {
        }

        internal Proposal(LcdClient client)
        {
            _client = client;
        }

        public long Id { get; set; }
        public Content Content { get; set; }
        public ProposalStatus Status { get; set; }
        public Tally FinalTallyResult { get; set; }
        public DateTime SubmitTime { get; set; }
        public DateTime DepositEndTime { get; set; }
        public List<Coin> TotalDeposit { get; set; }
        public DateTime VotingStartTime { get; set; }
        public DateTime VotingEndTime { get; set; }

        internal Task<PaginatedResult<Proposal>> GetAll(string paginationKey = null, int? pageNumber = null, bool? getTotalCount = null, bool? isDescending = null)
        {
            return _client.GetPaginatedResult(
                "/cosmos/gov/v1beta1/proposals",
                new
                {
                    Proposals = new List<Proposal>(),
                    Pagination = new Pagination()
                },
                data => data.Pagination.BuildResult(data.Proposals, pageNumber),
                paginationKey, pageNumber, getTotalCount, isDescending);
        }

        internal Task<Result<Proposal>> Get(long proposalId)
        {
            return _client.GetResult(
                $"/cosmos/gov/v1beta1/proposals/{proposalId}",
                new { Proposal = new Proposal() },
                data => new Result<Proposal> { Value = data.Proposal });
        }
    }
}
