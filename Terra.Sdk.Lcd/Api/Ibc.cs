using System.Threading.Tasks;
using Terra.Sdk.Lcd.Extensions;
using Terra.Sdk.Lcd.Models;
using Terra.Sdk.Lcd.Models.Entities.Ibc;

namespace Terra.Sdk.Lcd.Api
{
    public class Ibc
    {
        private readonly LcdClient _client;

        internal Ibc(LcdClient client)
        {
            _client = client;
        }

        public Task<Result<IbcClientParams>> GetParameters() => new IbcClientParams(_client).Get();

        public Task<PaginatedResult<IdentifiedClientState>> GetClientStates(string paginationKey = null, int? pageNumber = null, bool? getTotalCount = null, bool? isDescending = null) =>
            new IdentifiedClientState(_client).GetAll();

        public Task<Result<IdentifiedClientState>> GetClientState(string clientId) => new IdentifiedClientState(_client).Get(clientId);

        public Task<Result<string>> GetClientStatus(string clientId)
        {
            return _client.GetResult(
                $"/ibc/core/client/v1/client_status/{clientId}",
                new {Status = ""},
                data => new Result<string> {Value = data.Status});
        }

        public Task<PaginatedResult<ConsensusStateWithHeight>> GetConsensusStates(string clientId, string paginationKey = null, int? pageNumber = null, bool? getTotalCount = null, bool? isDescending = null) =>
            new ConsensusStateWithHeight(_client).Get(clientId, paginationKey, pageNumber, getTotalCount, isDescending);
    }
}
