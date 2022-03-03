using System.Collections.Generic;
using System.Threading.Tasks;
using Terra.Sdk.Lcd.Extensions;

namespace Terra.Sdk.Lcd.Models.Entities.Ibc
{
    public class DenomTrace
    {
        private readonly LcdClient _client;

        /// <remarks>
        /// For serialization.
        /// </remarks>
        public DenomTrace()
        {
        }

        internal DenomTrace(LcdClient client)
        {
            _client = client;
        }

        public string Path { get; set; }
        public string BaseDenom { get; set; }

        internal Task<Result<DenomTrace>> Get(string hash)
        {
            return _client.GetResult(
                $"/ibc/apps/transfer/v1/denom_traces/{hash}",
                new
                {
                    DenomTrace = new DenomTrace()
                },
                data => new Result<DenomTrace> { Value = data.DenomTrace });
        }

        internal Task<PaginatedResult<DenomTrace>> GetAll(
            string paginationKey = null, int? pageNumber = null, bool? getTotalCount = null, bool? isDescending = null)
        {
            return _client.GetPaginatedResult(
                "/ibc/apps/transfer/v1/denom_traces",
                new
                {
                    DenomTraces = new List<DenomTrace>(),
                    Pagination = new Pagination()
                },
                data => data.Pagination.BuildResult(data.DenomTraces, pageNumber),
                paginationKey, pageNumber, getTotalCount, isDescending);
        }
    }
}
