using System.Collections.Generic;
using System.Threading.Tasks;
using Terra.Sdk.Lcd.Extensions;

namespace Terra.Sdk.Lcd.Models.Entities
{
    public class SigningInfo
    {
        private readonly LcdClient _client;

        /// <remarks>
        /// For serialization.
        /// </remarks>
        public SigningInfo()
        {
        }

        internal SigningInfo(LcdClient client)
        {
            _client = client;
        }

        public string Address { get; set; }
        public string StartHeight { get; set; }
        public string IndexOffset { get; set; }
        public string JailedUntil { get; set; }
        public bool Tombstoned { get; set; }
        public string MissedBlocksCounter { get; set; }

        internal Task<Result<SigningInfo>> Get(string valConsAddress)
        {
            return _client.GetResult(
                $"/cosmos/slashing/v1beta1/signing_infos/{valConsAddress}",
                new {ValSigningInfo = new SigningInfo()},
                data => new Result<SigningInfo> {Value = data.ValSigningInfo});
        }

        internal Task<PaginatedResult<SigningInfo>> GetAll(string paginationKey = null, int? pageNumber = null, bool? getTotalCount = null, bool? isDescending = null)
        {
            return _client.GetPaginatedResult(
                "/cosmos/slashing/v1beta1/signing_infos",
                new {Info = new List<SigningInfo>(), Pagination = new Pagination()},
                data => data.Pagination.BuildResult(data.Info, pageNumber),
                paginationKey, pageNumber, getTotalCount, isDescending);
        }
    }
}
