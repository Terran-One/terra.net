using System.Threading.Tasks;
using Terra.Sdk.Lcd.Extensions;

namespace Terra.Sdk.Lcd.Models.Entities
{
    public class MintingParams
    {
        private readonly LcdClient _client;

        /// <remarks>
        /// For serialization.
        /// </remarks>
        public MintingParams()
        {
        }

        internal MintingParams(LcdClient client)
        {
            _client = client;
        }

        public string MintDenom { get; set; }
        public string InflationRateChange { get; set; }
        public string InflationMax { get; set; }
        public string InflationMin { get; set; }
        public string GoalBonded { get; set; }
        public string BlocksPerYear { get; set; }

        internal Task<Result<MintingParams>> Get()
        {
            return _client.GetResult(
                "/cosmos/mint/v1beta1/params",
                new {Params = new MintingParams()},
                data => new Result<MintingParams> {Value = data.Params});
        }
    }
}