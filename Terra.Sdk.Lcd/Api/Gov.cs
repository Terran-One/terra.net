using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Terra.Sdk.Lcd.Extensions;
using Terra.Sdk.Lcd.Models;
using Terra.Sdk.Lcd.Models.Entities;

namespace Terra.Sdk.Lcd.Api
{
    public class Gov
    {
        private readonly LcdClient _client;

        internal Gov(LcdClient client)
        {
            _client = client;
        }

        public Task<PaginatedResult<Proposal>> GetProposals(
            string paginationKey = null, int? pageNumber = null, bool? getTotalCount = null, bool? isDescending = null)
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

        public Task<Result<Proposal>> GetProposal(long proposalId)
        {
            throw new NotImplementedException();
        }

        public Task<Result<string>> GetProposer(long proposalId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Coin>> GetInitialDeposit(long proposalId)
        {
            throw new NotImplementedException();
        }

        public Task<PaginatedResult<Deposit>> GetDeposits(
            long proposalId, string paginationKey = null, int? pageNumber = null, bool? getTotalCount = null, bool? isDescending = null)
        {
            throw new NotImplementedException();
        }

        public Task<Result<Tx>> SearchProposalCreationTx(long proposalId)
        {
            throw new NotImplementedException();
        }

        public Task<PaginatedResult<Vote>> GetVotes(
            long proposalId, string paginationKey = null, int? pageNumber = null, bool? getTotalCount = null, bool? isDescending = null)
        {
            throw new NotImplementedException();
        }

        public Task<Result<Tally>> GetTally(long proposalId)
        {
            throw new NotImplementedException();
        }

        public Task<Result<DepositParams>> GetDepositParameters()
        {
            throw new NotImplementedException();
        }

        public Task<Result<VotingParams>> GetVotingParameters()
        {
            throw new NotImplementedException();
        }

        public Task<Result<TallyParams>> GetTallyParameters()
        {
            throw new NotImplementedException();
        }

        public Task<Result<GovParams>> GetGovParameters()
        {
            throw new NotImplementedException();
        }
    }
}
