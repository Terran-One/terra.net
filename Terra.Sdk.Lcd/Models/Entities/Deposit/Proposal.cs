using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProtoBuf;
using Terra.Sdk.Lcd.Extensions;

namespace Terra.Sdk.Lcd.Models.Entities.Deposit
{
    [ProtoContract]
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

        [ProtoMember(1, Name = "id")] public long Id { get; set; }
        [ProtoMember(2, Name = "content")] public Content.Content Content { get; set; }
        [ProtoMember(3, Name = "status")] public ProposalStatus Status { get; set; }
        [ProtoMember(4, Name = "final_tally_result")] public Tally FinalTallyResult { get; set; }
        [ProtoMember(5, Name = "submit_time")] public DateTime SubmitTime { get; set; }
        [ProtoMember(6, Name = "deposit_end_time")] public DateTime DepositEndTime { get; set; }
        [ProtoMember(7, Name = "total_deposit")] public List<Coin> TotalDeposit { get; set; }
        [ProtoMember(8, Name = "voting_start_time")] public DateTime VotingStartTime { get; set; }
        [ProtoMember(9, Name = "voting_end_time")] public DateTime VotingEndTime { get; set; }

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
                new {Proposal = new Proposal()},
                data => new Result<Proposal> {Value = data.Proposal});
        }
    }
}
