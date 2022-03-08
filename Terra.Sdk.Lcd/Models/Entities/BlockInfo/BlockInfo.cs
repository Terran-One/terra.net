using System.Threading.Tasks;
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

        public Task<Result<BlockInfo>> Get(long? height = null)
        {
            return _client.GetResult<BlockInfo>($"/cosmos/base/tendermint/v1beta1/blocks/{(height.HasValue ? height.ToString() : "latest")}");
        }
    }
}
