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

        [ProtoMember(1)] public long Id { get; set; }
        [ProtoMember(2)] public Content.Content Content { get; set; }
        [ProtoMember(3)] public ProposalStatus Status { get; set; }
        [ProtoMember(4)] public Tally FinalTallyResult { get; set; }
        [ProtoMember(5)] public DateTime SubmitTime { get; set; }
        [ProtoMember(6)] public DateTime DepositEndTime { get; set; }
        [ProtoMember(7)] public List<Coin> TotalDeposit { get; set; }
        [ProtoMember(8)] public DateTime VotingStartTime { get; set; }
        [ProtoMember(9)] public DateTime VotingEndTime { get; set; }

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