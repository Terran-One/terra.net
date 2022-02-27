using System;
using System.Threading.Tasks;
using Terra.Sdk.Lcd.Models;
using Terra.Sdk.Lcd.Models.Entities;

namespace Terra.Sdk.Lcd.Api
{
    public class Ibc
    {
        private readonly LcdClient _client;

        public Ibc(LcdClient client)
        {
            _client = client;
        }

        public async Task<Result<IbcClientParams>> GetParameters()
        {
            throw new NotImplementedException();
        }

        public async Task<PaginatedResult<IdentifiedClientState>> GetClientStates(
            string paginationKey = null, int? pageNumber = null, bool? getTotalCount = null, bool? isDescending = null)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<IdentifiedClientState>> GetClientState(string clientId)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<string>> GetClientStatus(string clientId)
        {
            throw new NotImplementedException();
        }

        public async Task<PaginatedResult<ClientConsensusStates>> GetConsensusStates(string clientId,
            string paginationKey = null, int? pageNumber = null, bool? getTotalCount = null, bool? isDescending = null)
        {
            throw new NotImplementedException();
        }
    }
}