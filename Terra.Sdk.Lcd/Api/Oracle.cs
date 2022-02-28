using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Terra.Sdk.Lcd.Models;
using Terra.Sdk.Lcd.Models.Entities;

namespace Terra.Sdk.Lcd.Api
{
    public class Oracle
    {
        private readonly LcdClient _client;

        public Oracle(LcdClient client)
        {
            _client = client;
        }

        public async Task<Result<List<Coin>>> GetExchangeRates()
        {
            throw new NotImplementedException();
        }

        public async Task<Result<Coin>> GetExchangeRate(string denom)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<List<string>>> GetActiveDenoms()
        {
            throw new NotImplementedException();
        }

        public async Task<Result<string>> GetFeederAddress(string validator)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<long>> GetMisses(string validator)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<AggregateExchangeRatePrevote>> GetAggregatePrevote(string validator)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<AggregateExchangeRateVote>> GetAggregateVote(string validator)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<OracleParams>> GetParameters()
        {
            throw new NotImplementedException();
        }
    }
}