using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using Terra.Sdk.Lcd.Extensions;
using Terra.Sdk.Lcd.Models.Entities.Tx.Msg.GovMsg;

namespace Terra.Sdk.Lcd.Models.Entities.Deposit
{
    public class Deposit
    {
        private readonly LcdClient _client;

        /// <remarks>
        /// For serialization
        /// </remarks>
        public Deposit()
        {
        }

        internal Deposit(LcdClient client)
        {
            _client = client;
        }

        public string ProposalId { get; set; }
        public string Depositor { get; set; }
        public List<Coin> Amount { get; set; }

        public async Task<PaginatedResult<Deposit>> GetByProposal(long proposalId,
            string paginationKey = null, int? pageNumber = null, bool? getTotalCount = null, bool? isDescending = null)
        {
            var proposalResult = await new Proposal(_client).Get(proposalId);
            if (proposalResult.Error != null)
                return new PaginatedResult<Deposit> { Error = proposalResult.Error };

            var proposal = proposalResult.Value;
            if (proposal.Status == ProposalStatus.DepositPeriod || proposal.Status == ProposalStatus.VotingPeriod)
            {
                return await _client.GetPaginatedResult(
                    $"/cosmos/gov/v1beta1/proposals/{proposalId}/deposits",
                    new
                    {
                        Deposits = new List<Deposit>(),
                        Pagination = new Pagination()
                    },
                    data => data.Pagination.BuildResult(data.Deposits, pageNumber),
                    paginationKey, pageNumber, getTotalCount, isDescending);
            }

            // build search params
            var @params = new StringBuilder();
            @params.Append($"events={HttpUtility.UrlEncode("message.action='/cosmos.gov.v1beta1.MsgDeposit'")}");
            @params.Append($"&events={HttpUtility.UrlEncode($"proposal_deposit.proposal_id={proposalId}")}");

            var queryString = string.Join("&", @params, _client.GetPaginationQueryString(paginationKey, pageNumber, getTotalCount, isDescending)).TrimEnd('&');
            if (!string.IsNullOrWhiteSpace(queryString))
                queryString = $"?{queryString}";

            var response = await _client.HttpClient.GetAsync($"/cosmos/tx/v1beta1/txs{queryString}");
            if (!response.IsSuccessStatusCode)
                return await response.GetPaginatedErrorResult<Deposit>();

            var value = JsonConvert.DeserializeAnonymousType(
                await response.Content.ReadAsStringAsync(),
                new
                {
                    Txs = new List<Models.Entities.Tx.Tx>(),
                    Pagination = new Pagination()
                },
                Global.JsonSerializerSettings);

            var deposits = new List<Deposit>();
            var messages = value.Txs.SelectMany(tx => tx.Body.Messages);
            foreach (var msg in messages)
            {
                if (msg is MsgDeposit msgDeposit && int.Parse(msgDeposit.ProposalId) == proposalId)
                {
                    deposits.Add(new Deposit
                    {
                        ProposalId = proposalId.ToString(),
                        Depositor = msgDeposit.Depositor,
                        Amount = msgDeposit.Amount
                    });
                }
            }

            return value.Pagination.BuildResult(deposits, pageNumber);
        }
    }
}
