using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Terra.Sdk.Lcd.Extensions;
using Terra.Sdk.Lcd.Models;
using Terra.Sdk.Lcd.Models.Entities;
using Terra.Sdk.Lcd.Models.Entities.BlockInfo;

namespace Terra.Sdk.Lcd.Api
{
    public class Tendermint
    {
        private readonly LcdClient _client;

        internal Tendermint(LcdClient client)
        {
            _client = client;
        }

        public async Task<Result<JObject>> GetNodeInfo()
        {
            var response = await _client.HttpClient.GetAsync("/cosmos/base/tendermint/v1beta1/node_info");
            if (!response.IsSuccessStatusCode)
                return await response.GetErrorResult<JObject>();

            using (var stringReader = new StringReader(await response.Content.ReadAsStringAsync()))
            using (var jsonReader = new JsonTextReader(stringReader) {FloatParseHandling = FloatParseHandling.Decimal})
                return new Result<JObject> {Value = await JObject.LoadAsync(jsonReader, null)};
        }

        public Task<Result<bool>> GetSyncing()
        {
            return _client.GetResult(
                "/cosmos/base/tendermint/v1beta1/syncing",
                new {Synching = false},
                data => new Result<bool> {Value = data.Synching});
        }

        public Task<PaginatedResult<DelegateValidator>> GetValidatorSet(long? height = null, string paginationKey = null, int? pageNumber = null, bool? getTotalCount = null, bool? isDescending = null) =>
            new DelegateValidator(_client).Get(height, paginationKey, pageNumber, getTotalCount, isDescending);

        public Task<Result<BlockInfo>> GetBlockInfo(long? height = null) => new BlockInfo(_client).Get(height);
    }
}
