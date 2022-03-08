using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using Terra.Sdk.Lcd.Extensions;

namespace Terra.Sdk.Lcd.Models.Entities.Staking
{
    public class Redelegation
    {
        private readonly LcdClient _client;

        /// <remarks>
        /// For serialization.
        /// </remarks>
        public Redelegation()
        {
        }

        [JsonProperty("redelegation")] public RedelegationDetails Details { get; set; }
        public List<RedelegationEntry> Entries { get; set; }

        internal Redelegation(LcdClient client)
        {
            _client = client;
        }

        internal Task<PaginatedResult<Redelegation>> GetAll(string delegator, string validatorSrc, string validatorDst,
            string paginationKey = null, int? pageNumber = null, bool? getTotalCount = null, bool? isDescending = null)
        {
            var additionalParams = $"src_validator_addr={HttpUtility.UrlEncode(validatorSrc)}&dst_validator_addr={HttpUtility.UrlEncode(validatorDst)}";
            return _client.GetPaginatedResult(
                $"/cosmos/staking/v1beta1/delegators/{delegator}/redelegations",
                new {RedelegationResponses = new List<Redelegation>(), Pagination = new Pagination()},
                data => data.Pagination.BuildResult(data.RedelegationResponses, pageNumber),
                paginationKey, pageNumber, getTotalCount, isDescending,
                additionalParams);
        }
    }
}