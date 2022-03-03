using System;
using System.Threading.Tasks;

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
            throw new NotImplementedException();
        }
    }
}