using System.Collections.Generic;
using System.Threading.Tasks;
using Terra.Sdk.Lcd.Extensions;
using Terra.Sdk.Lcd.Models.Entities.Tx.Msg.IbcChannelMsg.Primitives;

namespace Terra.Sdk.Lcd.Models.Entities.Ibc
{
    public class ConsensusStateWithHeight
    {
        private readonly LcdClient _client;

        public ConsensusStateWithHeight()
        {
        }

        public ConsensusStateWithHeight(LcdClient client)
        {
            _client = client;
        }

        public Height Height { get; set; }
        public dynamic ConsensusState { get; set; }

        internal Task<PaginatedResult<ConsensusStateWithHeight>> Get(string clientId,
            string paginationKey = null, int? pageNumber = null, bool? getTotalCount = null, bool? isDescending = null)
        {
            return _client.GetPaginatedResult(
                $"/ibc/core/client/v1/consensus_states/{clientId}",
                new
                {
                    ConsensusStates = new List<ConsensusStateWithHeight>(),
                    Pagination = new Pagination()
                },
                data => data.Pagination.BuildResult(data.ConsensusStates, pageNumber),
                paginationKey, pageNumber, getTotalCount, isDescending);
        }
    }
}
