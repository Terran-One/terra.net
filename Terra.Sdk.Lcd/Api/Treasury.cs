using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Terra.Sdk.Lcd.Extensions;
using Terra.Sdk.Lcd.Models;
using Terra.Sdk.Lcd.Models.Entities;

namespace Terra.Sdk.Lcd.Api
{
    public class Treasury
    {
        private readonly LcdClient _client;

        /// <remarks>
        /// For serialization.
        /// </remarks>
        public Treasury()
        {
        }

        internal Treasury(LcdClient client)
        {
            _client = client;
        }

        public Task<Result<List<Coin>>> GetTaxCaps()
        {
            return _client.GetResult(
                "/terra/treasury/v1beta1/tax_caps",
                new {TaxCaps = new[] { new { Denom = "", TaxCap = 0M }} },
                data => new Result<List<Coin>> {Value = data.TaxCaps.Select(d => new Coin(d.Denom, d.TaxCap)).ToList()});
        }

        public Task<Result<Coin>> GetTaxCap(string denom)
        {
            return _client.GetResult(
                $"/terra/treasury/v1beta1/tax_caps/{denom}",
                new {TaxCap = 0M},
                data => new Result<Coin> {Value = new Coin(denom, data.TaxCap)});
        }

        public Task<Result<decimal>> GetTaxRate(long? height = null)
        {
            var query = height.HasValue ? $"?height={height.Value}" : "";
            return _client.GetResult(
                $"/terra/treasury/v1beta1/tax_rate{query}",
                new {TaxRate = 0M},
                data => new Result<decimal> {Value = data.TaxRate});
        }

        public Task<Result<decimal>> GetRewardWeight()
        {
            return _client.GetResult(
                "/terra/treasury/v1beta1/reward_weight",
                new {RewardWeight = 0M},
                data => new Result<decimal> {Value = data.RewardWeight});
        }

        public Task<Result<List<Coin>>> GetTaxProceeds()
        {
            return _client.GetResult(
                "/terra/treasury/v1beta1/tax_proceeds",
                new {TaxProceeds = new List<Coin>() },
                data => new Result<List<Coin>> {Value = data.TaxProceeds});
        }

        public Task<Result<Coin>> GetSeigniorageProceeds()
        {
            return _client.GetResult(
                "/terra/treasury/v1beta1/seigniorage_proceeds",
                new {SeigniorageProceeds = 0M},
                data => new Result<Coin> {Value = new Coin("uluna", data.SeigniorageProceeds)});
        }

        public Task<Result<TreasuryParams>> GetParameters() => new TreasuryParams(_client).Get();
    }
}
