using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Terra.Sdk.Lcd.Models.Entities.BlockInfo
{
    public class BlockInfo
    {
        private readonly LcdClient _lcdClient;

        /// <remarks>
        /// For serialization.
        /// </remarks>
        public BlockInfo()
        {
        }

        internal BlockInfo(LcdClient lcdClient)
        {
            _lcdClient = lcdClient;
        }

        public BlockId BlockId { get; set; }
        public Block Block { get; set; }

        public async Task<Result<BlockInfo>> Get(long? height = null)
        {
            var response = await _lcdClient.HttpClient.GetAsync($"/cosmos/base/tendermint/v1beta1/blocks/{(height.HasValue ? height.ToString() : "latest")}");
            if (!response.IsSuccessStatusCode)
                return new Result<BlockInfo> {  Error = $"Fetch failed: {response.ReasonPhrase}" };

            var blockInfo = JsonConvert.DeserializeObject<BlockInfo>(await response.Content.ReadAsStringAsync(), _lcdClient.JsonSerializerSettings);
            return new Result<BlockInfo> { Value = blockInfo };
        }
    }
}
