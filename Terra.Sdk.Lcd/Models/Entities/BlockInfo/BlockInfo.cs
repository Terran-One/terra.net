using System.Threading.Tasks;
using Newtonsoft.Json;
using Terra.Sdk.Lcd.Extensions;

namespace Terra.Sdk.Lcd.Models.Entities.BlockInfo
{
    public class BlockInfo
    {
        private readonly LcdClient _client;

        /// <remarks>
        /// For serialization.
        /// </remarks>
        public BlockInfo()
        {
        }

        internal BlockInfo(LcdClient lcdClient)
        {
            _client = lcdClient;
        }

        public BlockId BlockId { get; set; }
        public Block Block { get; set; }

        public async Task<Result<BlockInfo>> Get(long? height = null)
        {
            var response = await _client.HttpClient.GetAsync($"/cosmos/base/tendermint/v1beta1/blocks/{(height.HasValue ? height.ToString() : "latest")}");
            if (!response.IsSuccessStatusCode)
                return new Result<BlockInfo> {  Error = await response.GetErrorString() };

            var blockInfo = JsonConvert.DeserializeObject<BlockInfo>(await response.Content.ReadAsStringAsync(), _client.JsonSerializerSettings);
            return new Result<BlockInfo> { Value = blockInfo };
        }
    }
}
