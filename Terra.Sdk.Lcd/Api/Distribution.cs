using System.Collections.Generic;
using System.Threading.Tasks;
using Terra.Sdk.Lcd.Extensions;
using Terra.Sdk.Lcd.Models;
using Terra.Sdk.Lcd.Models.Entities;
using Terra.Sdk.Lcd.Models.Entities.Rewards;

namespace Terra.Sdk.Lcd.Api
{
    public class Distribution
    {
        private readonly LcdClient _client;

        internal Distribution(LcdClient lcdClient)
        {
            _client = lcdClient;
        }

        public Task<Result<Rewards>> GetRewards(string delegator) => new Rewards(_client).Get(delegator);

        public Task<Result<List<Coin>>> GetValidatorCommission(string validator)
        {
            return _client.GetResult(
                $"/cosmos/distribution/v1beta1/validators/{validator}/commission",
                new {Commission = new {Commission = new List<Coin>()}},
                data => new Result<List<Coin>> {Value = data.Commission.Commission});
        }

        public Task<Result<string>> GetWithdrawAddress(string delegator)
        {
            return _client.GetResult(
                $"/cosmos/distribution/v1beta1/delegators/{delegator}/withdraw_address",
                new {WithdrawAddress = ""},
                data => new Result<string> {Value = data.WithdrawAddress});
        }

        public Task<Result<List<Coin>>> GetCommunityPool()
        {
            return _client.GetResult(
                "/cosmos/distribution/v1beta1/community_pool",
                new {Pool = new List<Coin>()},
                data => new Result<List<Coin>> {Value = data.Pool});
        }

        public Task<Result<DistributionParameters>> GetParameters() => new DistributionParameters(_client).Get();
    }
}