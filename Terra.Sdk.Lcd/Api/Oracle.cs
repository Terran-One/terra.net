using System.Collections.Generic;
using System.Threading.Tasks;
using Terra.Sdk.Lcd.Extensions;
using Terra.Sdk.Lcd.Models;
using Terra.Sdk.Lcd.Models.Entities.Oracle;

namespace Terra.Sdk.Lcd.Api
{
    public class Oracle
    {
        private readonly LcdClient _client;

        internal Oracle(LcdClient client)
        {
            _client = client;
        }

        public Task<Result<List<Coin>>> GetExchangeRates()
        {
            return _client.GetResult(
                "/terra/oracle/v1beta1/denoms/exchange_rates",
                new {ExchangeRates = new List<Coin>()},
                data => new Result<List<Coin>> {Value = data.ExchangeRates});
        }

        public Task<Result<Coin>> GetExchangeRate(string denom)
        {
            return _client.GetResult(
                $"/terra/oracle/v1beta1/denoms/{denom}/exchange_rate",
                new {ExchangeRate = 0M},
                data => new Result<Coin> {Value = new Coin(denom, data.ExchangeRate)});
        }

        public Task<Result<List<string>>> GetActiveDenoms()
        {
            return _client.GetResult(
                "/terra/oracle/v1beta1/denoms/active",
                new {Actives = new List<string>()},
                data => new Result<List<string>> {Value = data.Actives});
        }

        public Task<Result<string>> GetFeederAddress(string validator)
        {
            return _client.GetResult(
                $"/terra/oracle/v1beta1/validators/{validator}/feeder",
                new {FeederAddr = ""},
                data => new Result<string> {Value = data.FeederAddr});
        }

        public Task<Result<int>> GetMisses(string validator)
        {
            return _client.GetResult(
                $"/terra/oracle/v1beta1/validators/{validator}/miss",
                new {MissCounter = 0},
                data => new Result<int> {Value = data.MissCounter});
        }

        public Task<Result<AggregateExchangeRatePrevote>> GetAggregatePrevote(string validator) => new AggregateExchangeRatePrevote(_client).Get(validator);
        public Task<Result<AggregateExchangeRateVote>> GetAggregateVote(string validator) => new AggregateExchangeRateVote(_client).Get(validator);
        public Task<Result<OracleParams>> GetParameters() => new OracleParams(_client).Get();
    }
}
