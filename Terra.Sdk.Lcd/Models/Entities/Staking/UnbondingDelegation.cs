using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Terra.Sdk.Lcd.Extensions;

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

        internal Task<PaginatedResult<UnbondingDelegation>> GetAll(string delegator, string validator,
            string paginationKey = null, int? pageNumber = null, bool? getTotalCount = null, bool? isDescending = null)
        {
            if (delegator != null && validator != null)
            {
                return _client.GetPaginatedResult(
                    $"/cosmos/staking/v1beta1/validators/{validator}/delegations/${delegator}/unbonding_delegation",
                    new {Unbond = new UnbondingDelegation()},
                    data => new PaginatedResult<UnbondingDelegation> {Value = new[] {data.Unbond}.ToList()},
                    null, null, null, null);
            }

            if (delegator != null)
            {
                return _client.GetPaginatedResult(
                    $"/cosmos/staking/v1beta1/delegators/{delegator}/unbonding_delegations",
                    new {UnbondingResponses = new List<UnbondingDelegation>(), Pagination = new Pagination()},
                    data => data.Pagination.BuildResult(data.UnbondingResponses, pageNumber),
                    paginationKey, pageNumber, getTotalCount, isDescending);
            }

            if (validator != null)
            {
                return _client.GetPaginatedResult(
                    $"/cosmos/staking/v1beta1/validators/{validator}/unbonding_delegations",
                    new {UnbondingResponses = new List<UnbondingDelegation>(), Pagination = new Pagination()},
                    data => data.Pagination.BuildResult(data.UnbondingResponses, pageNumber),
                    paginationKey, pageNumber, getTotalCount, isDescending);
            }

            return Task.FromResult(
                new PaginatedResult<UnbondingDelegation> {Error = "arguments delegator and validator cannot both be empty"});
        }

        internal async Task<Result<UnbondingDelegation>> Get(string delegator, string validator)
        {
            var delegations = await GetAll(delegator, validator);
            if (!string.IsNullOrWhiteSpace(delegations.Error))
                return new Result<UnbondingDelegation> {Error = delegations.Error};

            return new Result<UnbondingDelegation> {Value = delegations.Value[0]};
        }
    }
}
