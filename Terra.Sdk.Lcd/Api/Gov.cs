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

        public async Task<Result<string>> GetProposer(long proposalId, long txHeight)
        {
            var creationTx = await SearchProposalCreationTx(proposalId, txHeight);
            if (creationTx.Error != null)
                return new Result<string> {Error = creationTx.Error};

            var msg = creationTx.Value.Body.Messages.OfType<MsgSubmitProposal>().SingleOrDefault();
            if (msg == null)
                return new Result<string> {Error = new Error {Message = "Failed to fetch submit_proposer tx"}};

            return new Result<string> {Value = msg.Proposer};
        }

        public async Task<Result<List<Coin>>> GetInitialDeposit(long proposalId, long txHeight)
        {
            var creationTx = await SearchProposalCreationTx(proposalId, txHeight);
            if (creationTx.Error != null)
                return new Result<List<Coin>> {Error = creationTx.Error};

            var msg = creationTx.Value.Body.Messages.OfType<MsgSubmitProposal>().SingleOrDefault();
            if (msg == null)
                return new Result<List<Coin>> {Error = new Error {Message = "Failed to fetch submit_proposer tx"}};

            return new Result<List<Coin>> {Value = msg.InitialDeposit};
        }

        public Task<PaginatedResult<Deposit>> GetDeposits(long proposalId, long? txHeight = null, string paginationKey = null, int? pageNumber = null, bool? getTotalCount = null, bool? isDescending = null) =>
            new Deposit(_client).GetByProposal(proposalId, txHeight, paginationKey, pageNumber, getTotalCount, isDescending);

        public Task<Result<Models.Entities.Tx.Tx>> SearchProposalCreationTx(long proposalId, long txHeight) => new Models.Entities.Tx.Tx(_client).GetByProposal(proposalId, txHeight);

        public Task<PaginatedResult<Vote>> GetVotes(long proposalId, string paginationKey = null,
            int? pageNumber = null, bool? getTotalCount = null, bool? isDescending = null) => new Vote(_client).GetByProposal(proposalId, paginationKey, pageNumber, getTotalCount, isDescending);

        public Task<Result<Tally>> GetTally(long proposalId) => Tally.GetByProposal(_client, proposalId);
        public Task<Result<DepositParams>> GetDepositParameters() => new DepositParams(_client).Get();
        public Task<Result<VotingParams>> GetVotingParameters() => new VotingParams(_client).Get();
        public Task<Result<TallyParams>> GetTallyParameters() => new TallyParams(_client).Get();
    }
}
