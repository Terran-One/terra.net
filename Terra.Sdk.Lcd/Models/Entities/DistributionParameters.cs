using System.Threading.Tasks;
using Terra.Sdk.Lcd.Extensions;

namespace Terra.Sdk.Lcd.Models.Entities
{
    public class DistributionParameters
    {
        private readonly LcdClient _client;

        /// <remarks>
        /// For serialization.
        /// </remarks>
        public DistributionParameters()
        {
        }

        internal DistributionParameters(LcdClient client)
        {
            _client = client;
        }

        public string CommunityTax { get; set; }
        public string BaseProposerReward { get; set; }
        public string BonusProposerReward { get; set; }
        public bool WithdrawAddrEnabled { get; set; }

        internal Task<Result<DistributionParameters>> Get()
        {
            return _client.GetResult<DistributionParameters>("/cosmos/distribution/v1beta1/params");
        }
    }
}
