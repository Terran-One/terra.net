using System.Collections.Generic;

namespace Terra.Sdk.Lcd.Models.Entities.Rewards
{
    public class ValidatorReward
    {
        public string ValidatorAddress { get; set; }
        public List<Coin> Reward { get; set; }
    }
}