using System.Threading.Tasks;
using Terra.Sdk.Lcd.Extensions;

namespace Terra.Sdk.Lcd.Models.Entities
{
    public class MarketParams
    {
        private readonly LcdClient _client;

        /// <remarks>
        /// For serialization.
        /// </remarks>
        public MarketParams()
        {
        }

        internal MarketParams(LcdClient client)
        {
            _client = client;
        }

        public string PoolRecoveryPeriod { get; set; }
        public string BasePool { get; set; }
        public string MinStabilitySpread { get; set; }

        internal Task<Result<MarketParams>> Get()
        {
            return _client.GetResult(
                "/terra/market/v1beta1/params",
                new {Params = new MarketParams()},
                data => new Result<MarketParams> {Value = data.Params});
        }
    }
}