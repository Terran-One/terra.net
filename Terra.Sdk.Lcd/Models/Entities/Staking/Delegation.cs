using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Terra.Sdk.Lcd.Extensions;

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

        internal Task<PaginatedResult<Delegation>> GetAll(string delegator, string validator, string paginationKey = null, int? pageNumber = null, bool? getTotalCount = null, bool? isDescending = null)
        {
            if (delegator != null && validator != null)
            {
                return _client.GetPaginatedResult(
                    $"/cosmos/staking/v1beta1/validators/${validator}/delegations/{delegator}",
                    new {DelegationResponse = new Delegation()},
                    data => new PaginatedResult<Delegation> {Value = new[] {data.DelegationResponse}.ToList()},
                    null, null, null, null);
            }

            if (delegator != null)
            {
                return _client.GetPaginatedResult(
                    $"/cosmos/staking/v1beta1/delegations/{delegator}",
                    new {DelegationResponses = new List<Delegation>(), Pagination = new Pagination()},
                    data => data.Pagination.BuildResult(data.DelegationResponses, pageNumber),
                    paginationKey, pageNumber, getTotalCount, isDescending);
            }

            if (validator != null)
            {
                return _client.GetPaginatedResult(
                    $"/cosmos/staking/v1beta1/validators/{validator}/delegations",
                    new {DelegationResponses = new List<Delegation>(), Pagination = new Pagination()},
                    data => data.Pagination.BuildResult(data.DelegationResponses, pageNumber),
                    paginationKey, pageNumber, getTotalCount, isDescending);
            }

            return Task.FromResult(
                new PaginatedResult<Delegation> {Error = new Error {Message = "arguments delegator and validator cannot both be empty"}});
        }

        internal async Task<Result<Delegation>> Get(string delegator, string validator)
        {
            var delegations = await GetAll(delegator, validator);
            if (delegations.Error != null)
                return new Result<Delegation> {Error = delegations.Error};

            return new Result<Delegation> {Value = delegations.Value[0]};
        }
    }
}
