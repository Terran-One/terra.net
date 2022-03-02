using System.Collections.Generic;
using System.Threading.Tasks;
using Terra.Sdk.Lcd.Extensions;

namespace Terra.Sdk.Lcd.Models.Entities.Rewards
{
    public class Rewards
    {
        private readonly LcdClient _client;

        /// <remarks>
        /// For serialization.
        /// </remarks>
        public Rewards()
        {
        }

        internal Rewards(LcdClient client)
        {
            _client = client;
        }

        public List<ValidatorReward> ValidatorRewards { get; set; }
        public List<Coin> Total { get; set; }

        internal Task<Result<Rewards>> Get(string delegator)
        {
            return _client.GetResult<Rewards>($"/cosmos/distribution/v1beta1/delegators/{delegator}/rewards");
        }
    }
}
