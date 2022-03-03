using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using Terra.Sdk.Lcd.Extensions;
using Terra.Sdk.Lcd.Models.Entities.Tx;
using Terra.Sdk.Lcd.Models.Entities.Tx.Msg.GovMsg;
using Terra.Sdk.Lcd.Models.Entities.Tx.Msg.GovMsg.Primitives;

namespace Terra.Sdk.Lcd.Models.Entities.Deposit
{
    public class Vote
    {
        private readonly LcdClient _client;

        /// <remarks>
        /// For serialization.
        /// </remarks>
        public Vote()
        {
        }

        internal Vote(LcdClient client)
        {
            _client = client;
        }

        public long ProposalId { get; set; }
        public string Voter { get; set; }
        public List<WeightedVoteOption> Options { get; set; }

        public async Task<PaginatedResult<Vote>> GetByProposal(long proposalId,
            string paginationKey = null, int? pageNumber = null, bool? getTotalCount = null, bool? isDescending = null)
        {
            var proposalResult = await new Proposal(_client).Get(proposalId);
            if (!string.IsNullOrWhiteSpace(proposalResult.Error))
                return new PaginatedResult<Vote> { Error = proposalResult.Error };

            var proposal = proposalResult.Value;
            if (proposal.Status == ProposalStatus.DepositPeriod)
            {
                return await _client.GetPaginatedResult(
                    $"/cosmos/gov/v1beta1/proposals/{proposalId}/votes",
                    new
                    {
                        Votes = new List<Vote>(),
                        Pagination = new Pagination()
                    },
                    data => data.Pagination.BuildResult(data.Votes, pageNumber),
                    paginationKey, pageNumber, getTotalCount, isDescending);
            }

            // build search params
            var @params = new StringBuilder();
            @params.Append($"events={HttpUtility.UrlEncode("message.action='/cosmos.gov.v1beta1.MsgVote'")}");
            @params.Append($"&events={HttpUtility.UrlEncode($"proposal_vote.proposal_id={proposalId}")}");

            var queryString = string.Join("&", @params, _client.GetPaginationQueryString()).TrimEnd('&');
            if (!string.IsNullOrWhiteSpace(queryString))
                queryString = $"?{queryString}";

            var response = await _client.HttpClient.GetAsync($"/cosmos/tx/v1beta1/txs{queryString}");
            if (!response.IsSuccessStatusCode)
                return new PaginatedResult<Vote> { Error = $"Failed to fetch: {response.ReasonPhrase}"};

            var value = JsonConvert.DeserializeAnonymousType(
                await response.Content.ReadAsStringAsync(),
                new
                {
                    Txs = new List<Tx.Tx>(),
                    TxResponses = new List<TxInfo>(),
                    Pagination = new Pagination()

                },
                _client.JsonSerializerSettings);

            var votes = new List<Vote>();
            var messages = value.Txs.SelectMany(tx => tx.Body.Messages);
            foreach (var msg in messages)
            {
                if (msg is MsgVote msgVote && msgVote.ProposalId == proposalId)
                {
                    votes.Add(new Vote
                    {
                        ProposalId = proposalId,
                        Voter = msgVote.Voter,
                        Options = new[] { new WeightedVoteOption(msgVote.Option, 1)}.ToList()
                    });
                }
                else if (msg is MsgVoteWeighted msgVoteWeighted && msgVoteWeighted.ProposalId == proposalId)
                {
                    votes.Add(new Vote
                    {
                        ProposalId = proposalId,
                        Voter = msgVoteWeighted.Voter,
                        Options = msgVoteWeighted.Options
                    });
                }
            }

            return value.Pagination.BuildResult(votes, pageNumber);
        }
    }
}
