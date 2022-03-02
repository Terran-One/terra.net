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

        internal Treasury(LcdClient client)
        {
            _client = client;
        }

        public async Task<Result<List<Coin>>> GetTaxCaps()
        {
            throw new NotImplementedException();
        }

        public async Task<Result<Coin>> GetTaxCap(string denom)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<decimal>> GetTaxRate(long? height = null)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<decimal>> GetRewardHeight()
        {
            throw new NotImplementedException();
        }

        public async Task<Result<List<Coin>>> GetTaxProceeds()
        {
            throw new NotImplementedException();
        }

        public async Task<Result<Coin>> GetSeigniorageProceeds()
        {
            throw new NotImplementedException();
        }

        public async Task<Result<TreasuryParams>> GetParameters()
        {
            throw new NotImplementedException();
        }
    }
}
