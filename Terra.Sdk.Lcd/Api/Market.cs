using System;
using System.Threading.Tasks;
using System.Web;
using Terra.Sdk.Lcd.Extensions;
using Terra.Sdk.Lcd.Models;
using Terra.Sdk.Lcd.Models.Entities;

namespace Terra.Sdk.Lcd.Api
{
    public class Market
    {
        private readonly LcdClient _client;

        internal Market(LcdClient client)
        {
            _client = client;
        }

        public Task<Result<Coin>> GetSwapRate(Coin offerCoin, string askDenom)
        {
            return _client.GetResult(
                $"/terra/market/v1beta1/swap?offerCoin={HttpUtility.UrlEncode(offerCoin.ToString())}&askDenom={HttpUtility.UrlEncode(askDenom)}",
                new { ReturnCoin = new Coin() },
                data => new Result<Coin> { Value = data.ReturnCoin });
        }

        public Task<Result<decimal>> GetPoolDelta()
        {
            return _client.GetResult(
                "/terra/market/v1beta1/terra_pool_delta",
                new { TerraPoolDelta = 0M },
                data => new Result<decimal> { Value = data.TerraPoolDelta });
        }

        public Task<Result<MarketParams>> GetParameters() => new MarketParams(_client).Get();
    }
}
