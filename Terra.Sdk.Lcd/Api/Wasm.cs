using System;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Terra.Sdk.Lcd.Extensions;
using Terra.Sdk.Lcd.Models;
using Terra.Sdk.Lcd.Models.Entities.Wasm;

namespace Terra.Sdk.Lcd.Api
{
    public class Wasm
    {
        private readonly LcdClient _client;

        internal Wasm(LcdClient client)
        {
            _client = client;
        }

        public Task<Result<CodeInfo>> GetCodeInfo(long codeId) => new CodeInfo(_client).Get(codeId);

        public Task<Result<ContractInfo>> GetContractInfo(string contractAddress) => new ContractInfo(_client).Get(contractAddress);

        public Task<Result<T>> GetContractQuery<T>(string contractAddress, object query) where T : new()
        {
            var queryMsg = Convert.ToBase64String(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(query, new JsonSerializerSettings
            {
                ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new SnakeCaseNamingStrategy()
                }
            })));
            return _client.GetResult(
                $"/terra/wasm/v1beta1/contracts/{contractAddress}/store?query_msg={queryMsg}",
                new {QueryResult = new T()},
                data => new Result<T> {Value = data.QueryResult});
        }

        public Task<Result<WasmParams>> GetParameters() => new WasmParams(_client).Get();
    }
}
