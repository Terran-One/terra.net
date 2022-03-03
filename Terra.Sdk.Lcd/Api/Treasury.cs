using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
            throw new NotImplementedException();
        }

        public Task<Result<Coin>> GetTaxCap(string denom)
        {
            throw new NotImplementedException();
        }

        public Task<Result<decimal>> GetTaxRate(long? height = null)
        {
            throw new NotImplementedException();
        }

        public Task<Result<decimal>> GetRewardHeight()
        {
            throw new NotImplementedException();
        }

        public Task<Result<List<Coin>>> GetTaxProceeds()
        {
            throw new NotImplementedException();
        }

        public Task<Result<Coin>> GetSeigniorageProceeds()
        {
            throw new NotImplementedException();
        }

        public Task<Result<TreasuryParams>> GetParameters() => new TreasuryParams(_client).Get();
    }
}
