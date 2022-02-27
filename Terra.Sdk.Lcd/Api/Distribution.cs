using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Terra.Sdk.Lcd.Models;
using Terra.Sdk.Lcd.Models.Entities;

namespace Terra.Sdk.Lcd.Api
{
    public class Distribution
    {
        private readonly LcdClient _lcdClient;

        public Distribution(LcdClient lcdClient)
        {
            _lcdClient = lcdClient;
        }

        public Task<Result<Rewards>> GetRewards(string delegator) => new Rewards(_lcdClient).Get(delegator);

        public async Task<Result<List<Coin>>> GetValidatorCommission(string validator)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<string>> GetWithdrawAddress(string delegator)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<List<Coin>>> GetCommunityPool()
        {
            throw new NotImplementedException();
        }

        public Task<Result<DistributionParameters>> GetDistributionParameters() =>
            new DistributionParameters(_lcdClient).Get();
    }
}