using System;
using System.Threading.Tasks;
using Terra.Sdk.Lcd.Models;
using Terra.Sdk.Lcd.Models.Entities;

namespace Terra.Sdk.Lcd.Api
{
    public class Wasm
    {
        private readonly LcdClient _client;

        internal Wasm(LcdClient client)
        {
            _client = client;
        }

        public async Task<Result<CodeInfo>> GetCodeInfo(long codeId)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<ContractInfo>> GetCContractInfo(string contractAddress)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<T>> GetContractQuery<T>(string contractAddress, object query)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<WasmParams>> GetParameters()
        {
            throw new NotImplementedException();
        }
    }
}
