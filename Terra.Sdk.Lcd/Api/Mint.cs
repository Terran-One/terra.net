using System;
using System.Threading.Tasks;
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
            throw new NotImplementedException();
        }

        public Task<Result<decimal>> GetAnnualProvisions()
        {
            throw new NotImplementedException();
        }

        public Task<Result<MintingParams>> GetParameters()
        {
            throw new NotImplementedException();
        }
    }
}
