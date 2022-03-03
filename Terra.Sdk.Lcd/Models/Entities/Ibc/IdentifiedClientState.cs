using System.Collections.Generic;
using System.Threading.Tasks;
using Terra.Sdk.Lcd.Extensions;

namespace Terra.Sdk.Lcd.Models.Entities.Ibc
{
    public class IdentifiedClientState
    {
        private readonly LcdClient _client;

        /// <remarks>
        /// For serialization.
        /// </remarks>
        public IdentifiedClientState()
        {
        }

        public IdentifiedClientState(LcdClient client)
        {
            _client = client;
        }

        public string ClientId { get; set; }
        public string ClientState { get; set; }

        internal Task<PaginatedResult<IdentifiedClientState>> GetAll(string paginationKey = null, int? pageNumber = null, bool? getTotalCount = null, bool? isDescending = null)
        {
            return _client.GetPaginatedResult(
                "/ibc/core/client/v1/client_states",
                new
                {
                   ClientStates = new List<IdentifiedClientState>(),
                   Pagination = new Pagination()
                },
                data => data.Pagination.BuildResult(data.ClientStates, pageNumber),
                paginationKey, pageNumber, getTotalCount, isDescending);
        }

        internal Task<Result<IdentifiedClientState>> Get(string clientId)
        {
            return _client.GetResult<IdentifiedClientState>($"/ibc/core/client/v1/client_states/{clientId}");
        }
    }
}
