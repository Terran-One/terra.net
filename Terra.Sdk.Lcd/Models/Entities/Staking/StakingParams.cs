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

        public string UnbondingTime { get; set; }
        public long MaxValidators { get; set; }
        public long MaxEntries { get; set; }
        public long HistoricalEntries { get; set; }
        public string BondDenom { get; set; }

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
}