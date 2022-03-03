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
}
