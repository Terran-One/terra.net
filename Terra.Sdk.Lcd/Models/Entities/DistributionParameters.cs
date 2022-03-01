using System;
using System.Threading.Tasks;

namespace Terra.Sdk.Lcd.Models.Entities
{
    public class DistributionParameters
    {
        private readonly LcdClient _client;

        internal DistributionParameters(LcdClient client)
        {
            _client = client;
        }

        public string CommunityTax { get; set; }
        public string BaseProposerReward { get; set; }
        public string BonusProposerReward { get; set; }
        public bool WithdrawAddrEnabled { get; set; }

        internal async Task<Result<DistributionParameters>> Get()
        {
            throw new NotImplementedException();
        }
    }
}
