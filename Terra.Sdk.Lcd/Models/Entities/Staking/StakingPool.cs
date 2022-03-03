using System.Threading.Tasks;
using Terra.Sdk.Lcd.Extensions;

namespace Terra.Sdk.Lcd.Models.Entities.Staking
{
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

        public string BondedTokens { get; set; }
        public string NotBondedTokens { get; set; }

        internal Task<Result<StakingPool>> Get()
        {
            return _client.GetResult(
                "/cosmos/staking/v1beta1/pool",
                new {Pool = new StakingPool()},
                data => new Result<StakingPool> {Value = data.Pool});
        }
    }
}
