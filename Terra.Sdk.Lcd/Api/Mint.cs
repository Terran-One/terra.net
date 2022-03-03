using System.Threading.Tasks;
using Terra.Sdk.Lcd.Extensions;
using Terra.Sdk.Lcd.Models;
using Terra.Sdk.Lcd.Models.Entities;

namespace Terra.Sdk.Lcd.Api
{
    public class Mint
    {
        private readonly LcdClient _client;

        internal Mint(LcdClient client)
        {
            _client = client;
        }

        public Task<Result<decimal>> GetInflation()
        {
            return _client.GetResult(
                "/cosmos/mint/v1beta1/inflation",
                new { Inflation = 0M },
                data => new Result<decimal> { Value = data.Inflation });
        }

        public Task<Result<decimal>> GetAnnualProvisions()
        {
            return _client.GetResult(
                "/cosmos/mint/v1beta1/annual_provisions",
                new { AnnualProvisions = 0M },
                data => new Result<decimal> { Value = data.AnnualProvisions });
        }

        public Task<Result<MintingParams>> GetParameters() => new MintingParams(_client).Get();
    }
}
