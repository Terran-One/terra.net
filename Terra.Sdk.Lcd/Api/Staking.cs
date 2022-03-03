using System.Threading.Tasks;
using Terra.Sdk.Lcd.Models;
using Terra.Sdk.Lcd.Models.Entities.Staking;

namespace Terra.Sdk.Lcd.Api
{
    public class Staking
    {
        private readonly LcdClient _client;

        internal Staking(LcdClient client)
        {
            _client = client;
        }

        public Task<PaginatedResult<Delegation>> GetDelegations(string delegator = null, string validator = null, string paginationKey = null, int? pageNumber = null, bool? getTotalCount = null, bool? isDescending = null) =>
            new Delegation(_client).GetAll(delegator, validator, paginationKey, pageNumber, getTotalCount, isDescending);

        public Task<Result<Delegation>> GetDelegation(string delegator, string validator) =>
            new Delegation(_client).Get(delegator, validator);

        public Task<PaginatedResult<UnbondingDelegation>> GetUnbondingDelegations(string delegator = null, string validator = null, string paginationKey = null, int? pageNumber = null, bool? getTotalCount = null, bool? isDescending = null) =>
            new UnbondingDelegation(_client).GetAll(delegator, validator, paginationKey, pageNumber, getTotalCount, isDescending);

        public Task<Result<UnbondingDelegation>> GetUnbondingDelegation(string delegator = null, string validator = null) =>
            new UnbondingDelegation(_client).Get(delegator, validator);

        public Task<PaginatedResult<Redelegation>> GetRedelegations(string delegator = null, string validatorSrc = null, string validatorDst = null, string paginationKey = null, int? pageNumber = null, bool? getTotalCount = null, bool? isDescending = null) =>
            new Redelegation(_client).GetAll(delegator, validatorSrc, validatorDst, paginationKey, pageNumber, getTotalCount, isDescending);

        public Task<PaginatedResult<Validator>> GetBondedValidators(string delegator = null,  string paginationKey = null, int? pageNumber = null, bool? getTotalCount = null, bool? isDescending = null) =>
            new Validator(_client).GetAllBonded(delegator, paginationKey, pageNumber, getTotalCount, isDescending);

        public Task<PaginatedResult<Validator>> GetValidators(string paginationKey = null, int? pageNumber = null, bool? getTotalCount = null, bool? isDescending = null) =>
            new Validator(_client).GetAll(paginationKey, pageNumber, getTotalCount, isDescending);

        public Task<Result<Validator>> GetValidator(string address) =>
            new Validator(_client).Get(address);

        public Task<Result<StakingPool>> GetPool() =>
            new StakingPool(_client).Get();

        public Task<Result<StakingParams>> GetParameters() => new StakingParams(_client).Get();
    }
}
