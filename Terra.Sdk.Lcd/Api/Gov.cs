using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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

        public async Task<PaginatedResult<Proposal>> GetProposals(string paginationKey = null, int? pageNumber = null,
            bool? getTotalCount = null, bool? isDescending = null)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<Proposal>> GetProposal(long proposalId)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<string>> GetProposer(long proposalId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Coin>> GetInitialDeposit(long proposalId)
        {
            throw new NotImplementedException();
        }

        public async Task<PaginatedResult<Deposit>> GetDeposits(long proposalId,
            string paginationKey = null, int? pageNumber = null, bool? getTotalCount = null, bool? isDescending = null)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<Tx>> SearchProposalCreationTx(long proposalId)
        {
            throw new NotImplementedException();
        }

        public async Task<PaginatedResult<Vote>> GetVotes(long proposalId,
            string paginationKey = null, int? pageNumber = null, bool? getTotalCount = null, bool? isDescending = null)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<Tally>> GetTally(long proposalId)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<DepositParams>> GetDepositParameters()
        {
            throw new NotImplementedException();
        }

        public async Task<Result<VotingParams>> GetVotingParameters()
        {
            throw new NotImplementedException();
        }

        public async Task<Result<TallyParams>> GetTallyParameters()
        {
            throw new NotImplementedException();
        }

        public async Task<Result<GovParams>> GetGovParameters()
        {
            throw new NotImplementedException();
        }
    }
}
