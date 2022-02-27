using System;
using System.Threading.Tasks;
using Terra.Sdk.Lcd.Models;
using Terra.Sdk.Lcd.Models.Entities;

namespace Terra.Sdk.Lcd.Api
{
    public class Market
    {
        private readonly LcdClient _client;

        public Market(LcdClient client)
        {
            _client = client;
        }

        public Task<Result<Coin>> GetSwapRate(Coin offerCoin, string askDenom)
        {
            throw new NotImplementedException();
        }

        public Task<Result<decimal>> GetPoolDelta()
        {
            throw new NotImplementedException();
        }

        public Task<Result<MarketParams>> GetParameters()
        {
            throw new NotImplementedException();
        }
    }
}