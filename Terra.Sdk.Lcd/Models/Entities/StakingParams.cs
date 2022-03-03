using System.Threading.Tasks;
using Terra.Sdk.Lcd.Extensions;

namespace Terra.Sdk.Lcd.Models.Entities
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
    }
}
