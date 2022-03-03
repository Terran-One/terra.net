using System;
using System.Threading.Tasks;

namespace Terra.Sdk.Lcd.Models.Entities.Staking
{
    public class Delegation
    {
        private readonly LcdClient _client;

        /// <remarks>
        /// For serialization.
        /// </remarks>
        public Delegation()
        {
        }

        internal Delegation(LcdClient client)
        {
            _client = client;
        }

        public string DelegatorAddress { get; set; }
        public string ValidatorAddress { get; set; }
        public string Shares { get; set; }

        internal Task<PaginatedResult<Delegation>> GetAll(string delegator, string validator, string paginationKey, int? pageNumber, bool? getTotalCount, bool? isDescending)
        {
            throw new NotImplementedException();
        }

        internal Task<Result<Delegation>> Get(string delegator, string validator)
        {
            throw new NotImplementedException();
        }
    }
}