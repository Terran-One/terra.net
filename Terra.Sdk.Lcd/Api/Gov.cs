using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Terra.Sdk.Lcd.Models;
using Terra.Sdk.Lcd.Models.Entities.Deposit;
using Terra.Sdk.Lcd.Models.Entities.Tx.Msg.GovMsg;

namespace Terra.Sdk.Lcd.Api
{
    public class Gov
    {
        private readonly LcdClient _client;

        internal Gov(LcdClient client)
        {
            _client = client;
        }

        public Task<PaginatedResult<Proposal>> GetProposals(string paginationKey = null, int? pageNumber = null, bool? getTotalCount = null, bool? isDescending = null) =>
            new Proposal(_client).GetAll(paginationKey, pageNumber, getTotalCount, isDescending);

        public Task<Result<Proposal>> GetProposal(long proposalId) => new Proposal(_client).Get(proposalId);

        public async Task<Result<string>> GetProposer(long proposalId)
        {
            var creationTx = await SearchProposalCreationTx(proposalId);
            if (!string.IsNullOrWhiteSpace(creationTx.Error))
                return new Result<string> { Error = creationTx.Error };

            var msg = creationTx.Value.Body.Messages.OfType<MsgSubmitProposal>().SingleOrDefault();
            if (msg == null)
                return new Result<string> { Error = "Failed to fetch submit_proposer tx" };

            return new Result<string> { Value = msg.Proposer };
        }

        public async Task<Result<List<Coin>>> GetInitialDeposit(long proposalId)
        {
            var creationTx = await SearchProposalCreationTx(proposalId);
            if (!string.IsNullOrWhiteSpace(creationTx.Error))
                return new Result<List<Coin>> { Error = creationTx.Error };

            var msg = creationTx.Value.Body.Messages.OfType<MsgSubmitProposal>().SingleOrDefault();
            if (msg == null)
                return new Result<List<Coin>> { Error = "Failed to fetch submit_proposer tx" };

            return new Result<List<Coin>> { Value = msg.InitialDeposit };
        }

        public Task<PaginatedResult<Deposit>> GetDeposits(long proposalId, string paginationKey = null, int? pageNumber = null, bool? getTotalCount = null, bool? isDescending = null) =>
            new Deposit(_client).ForProposal(proposalId, paginationKey, pageNumber, getTotalCount, isDescending);

        public Task<Result<Models.Entities.Tx.Tx>> SearchProposalCreationTx(long proposalId) => new Models.Entities.Tx.Tx(_client).ForProposal(proposalId);

        public Task<PaginatedResult<Vote>> GetVotes(long proposalId, string paginationKey = null,
            int? pageNumber = null, bool? getTotalCount = null, bool? isDescending = null) => new Vote(_client).ForProposal(proposalId, paginationKey, pageNumber, getTotalCount, isDescending);

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
