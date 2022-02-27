using System;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Terra.Sdk.Lcd.Models.Entities
{
    public class DistributionParameters
    {
        private readonly LcdClient _client;

        public DistributionParameters(LcdClient client)
        {
            _client = client;
        }

        [JsonProperty("community_tax")]
        public string CommunityTax { get; set; }

        [JsonProperty("base_proposer_reward")]
        public string BaseProposerReward { get; set; }

        [JsonProperty("bonus_proposer_reward")]
        public string BonusProposerReward { get; set; }

        [JsonProperty("withdraw_addr_enabled")]
        public bool WithdrawAddrEnabled { get; set; }

        public async Task<Result<DistributionParameters>> Get()
        {
            throw new NotImplementedException();
        }
    }
}