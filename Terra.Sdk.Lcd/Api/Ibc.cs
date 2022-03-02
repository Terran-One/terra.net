using System;
using System.Threading.Tasks;
using Terra.Sdk.Lcd.Models;
using Terra.Sdk.Lcd.Models.Entities;

namespace Terra.Sdk.Lcd.Api
{
    public class Ibc
    {
        private readonly LcdClient _client;

        internal Ibc(LcdClient client)
        {
            _client = client;
        }

        public Task<Result<IbcClientParams>> GetParameters()
        {
            throw new NotImplementedException();
        }

        public Task<PaginatedResult<IdentifiedClientState>> GetClientStates(
            string paginationKey = null, int? pageNumber = null, bool? getTotalCount = null, bool? isDescending = null)
        {
            throw new NotImplementedException();
        }

        public Task<Result<IdentifiedClientState>> GetClientState(string clientId)
        {
            throw new NotImplementedException();
        }

        public Task<Result<string>> GetClientStatus(string clientId)
        {
            throw new NotImplementedException();
        }

        public Task<PaginatedResult<ClientConsensusStates>> GetConsensusStates(string clientId,
            string paginationKey = null, int? pageNumber = null, bool? getTotalCount = null, bool? isDescending = null)
        {
            throw new NotImplementedException();
        }
    }
}
