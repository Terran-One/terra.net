using System.Threading.Tasks;
using Terra.Sdk.Lcd.Extensions;

namespace Terra.Sdk.Lcd.Models.Entities.Staking
{
    public class StakingParams
    {
        private readonly LcdClient _client;

        /// <remarks>
        /// For serialization.
        /// </remarks>
        public StakingParams()
        {
        }

        internal StakingParams(LcdClient client)
        {
            _client = client;
        }

        internal Task<Result<StakingParams>> Get()
        {
            return _client.GetResult(
                "/terra/wasm/staking/params",
                new {Params = new StakingParams()},
                data => new Result<StakingParams> {Value = data.Params});
        }
    }

    public class StakingPool
    {
        private readonly LcdClient _client;

        /// <remarks>
        /// For serialization.
        /// </remarks>
        public StakingPool()
        {
        }

        internal StakingPool(LcdClient client)
        {
            _client = client;
        }

        internal Task<Result<StakingPool>> Get()
        {
            throw new System.NotImplementedException();
        }
    }

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

        internal Task<PaginatedResult<Delegation>> GetAll(string delegator, string validator, string paginationKey, int? pageNumber, bool? getTotalCount, bool? isDescending)
        {
            throw new System.NotImplementedException();
        }

        internal Task<Result<Delegation>> Get(string delegator, string validator)
        {
            throw new System.NotImplementedException();
        }
    }

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

        internal Task<PaginatedResult<UnbondingDelegation>> GetAll(string delegator, string validator, string paginationKey, int? pageNumber, bool? getTotalCount, bool? isDescending)
        {
            throw new System.NotImplementedException();
        }

        internal Task<Result<UnbondingDelegation>> Get(string delegator, string validator)
        {
            throw new System.NotImplementedException();
        }
    }

    public class Redelegation
    {
        private readonly LcdClient _client;

        /// <remarks>
        /// For serialization.
        /// </remarks>
        public Redelegation()
        {
        }

        internal Redelegation(LcdClient client)
        {
            _client = client;
        }

        internal Task<PaginatedResult<Redelegation>> GetAll(string delegator, string validatorSrc, string validatorDst, string paginationKey, int? pageNumber, bool? getTotalCount, bool? isDescending)
        {
            throw new System.NotImplementedException();
        }
    }

    public class Validator
    {
        private readonly LcdClient _client;

        /// <remarks>
        /// For serialization.
        /// </remarks>
        public Validator()
        {
        }

        internal Validator(LcdClient client)
        {
            _client = client;
        }

        internal Task<PaginatedResult<Validator>> GetAllBonded(string delegator, string paginationKey, int? pageNumber, bool? getTotalCount, bool? isDescending)
        {
            throw new System.NotImplementedException();
        }

        internal Task<Result<Validator>> Get(string address)
        {
            throw new System.NotImplementedException();
        }

        internal Task<PaginatedResult<Validator>> GetAll(string paginationKey, int? pageNumber, bool? getTotalCount, bool? isDescending)
        {
            throw new System.NotImplementedException();
        }
    }
}
