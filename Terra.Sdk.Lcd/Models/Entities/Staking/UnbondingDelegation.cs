using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Terra.Sdk.Lcd.Models.Entities.Staking
{
    public class UnbondingDelegation
    {
        private readonly LcdClient _client;

        /// <remarks>
        /// For serialization.
        /// </remarks>
        public UnbondingDelegation()
        {
        }

        internal UnbondingDelegation(LcdClient client)
        {
            _client = client;
        }

        public string DelegatorAddress { get; set; }
        public string ValidatorAddress { get; set; }
        public List<UnbondingDelegartionEntry> Entries { get; set; }

        internal Task<PaginatedResult<UnbondingDelegation>> GetAll(string delegator, string validator, string paginationKey, int? pageNumber, bool? getTotalCount, bool? isDescending)
        {
            throw new NotImplementedException();
        }

        internal Task<Result<UnbondingDelegation>> Get(string delegator, string validator)
        {
            throw new NotImplementedException();
        }
    }
}