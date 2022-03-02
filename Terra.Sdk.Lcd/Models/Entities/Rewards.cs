using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Terra.Sdk.Lcd.Models.Entities
{
    public class Rewards
    {
        private readonly LcdClient _client;

        internal Rewards(LcdClient client)
        {
            _client = client;
        }

        public IDictionary<string, List<Coin>> ValidatorRewards { get; set; }
        public List<Coin> Total { get; set; }

        internal Task<Result<Rewards>> Get(string delegator)
        {
            throw new NotImplementedException();
        }
    }
}
