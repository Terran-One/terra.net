using System.Threading.Tasks;
using Terra.Sdk.Lcd.Extensions;

namespace Terra.Sdk.Lcd.Models.Entities
{
    public class TreasuryParams
    {
        private readonly LcdClient _client;

        /// <remarks>
        /// For serialization.
        /// </remarks>
        public TreasuryParams()
        {
        }

        public PolicyConstraints TaxPolicy { get; set; }
        public PolicyConstraints RewardPolicy { get; set; }
        public string SeigniorageBurdenTarget { get; set; }
        public string MiningIncrement { get; set; }
        public string WindowShort { get; set; }
        public string WindowLong { get; set; }
        public string WindowProbation { get; set; }

        internal TreasuryParams(LcdClient client)
        {
            _client = client;
        }

        internal Task<Result<TreasuryParams>> Get()
        {
            return _client.GetResult(
                "/terra/treasury/v1beta1/params",
                new {Params = new TreasuryParams()},
                data => new Result<TreasuryParams> {Value = data.Params});
        }

        public class PolicyConstraints
        {
            public string RateMin { get; set; }
            public string RateMax { get; set; }
            public Coin Cap { get; set; }
            public string ChangeRateMax { get; set; }
        }
    }
}
