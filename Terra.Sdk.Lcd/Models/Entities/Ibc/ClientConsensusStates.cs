using System.Collections.Generic;
using System.Threading.Tasks;
using Terra.Sdk.Lcd.Extensions;

namespace Terra.Sdk.Lcd.Models.Entities.Ibc
{
    public class ClientConsensusStates
    {
        private readonly LcdClient _client;

        /// <remarks>
        /// For serialization.
        /// </remarks>
        public ClientConsensusStates()
        {
        }

        internal ClientConsensusStates(LcdClient client)
        {
            _client = client;
        }

        public string ClientId { get; set; }
        public List<ConsensusStateWithHeight> ConsensusStates { get; set; }

        public Task<PaginatedResult<ClientConsensusStates>> Get(string clientId,
            string paginationKey = null, int? pageNumber = null, bool? getTotalCount = null, bool? isDescending = null)
        {
            return _client.GetPaginatedResult(
                $"/ibc/core/client/v1/consensus_states/{clientId}",
                new
                {
                    ConsensusStates = new List<ClientConsensusStates>(),
                    Pagination = new Pagination()
                },
                data => data.Pagination.BuildResult(data.ConsensusStates, pageNumber),
                paginationKey, pageNumber, getTotalCount, isDescending);
        }
    }
}
