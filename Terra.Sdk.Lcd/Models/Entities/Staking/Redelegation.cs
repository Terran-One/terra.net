using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;

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

        [JsonProperty("redelegation")]
        public RedelegationDetails Details { get; set; }
        public List<RedelegationEntry> Entries { get; set; }

        internal Redelegation(LcdClient client)
        {
            _client = client;
        }

        internal Task<PaginatedResult<Redelegation>> GetAll(string delegator, string validatorSrc, string validatorDst, string paginationKey, int? pageNumber, bool? getTotalCount, bool? isDescending)
        {
            throw new NotImplementedException();
        }
    }
}