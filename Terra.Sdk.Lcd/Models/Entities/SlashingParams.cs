using System.Threading.Tasks;
using Terra.Sdk.Lcd.Extensions;

namespace Terra.Sdk.Lcd.Models.Entities
{
    public class SlashingParams
    {
        private readonly LcdClient _client;

        /// <remarks>
        /// For serialization.
        /// </remarks>
        public SlashingParams()
        {
        }

        internal SlashingParams(LcdClient client)
        {
            _client = client;
        }

        public string SignedBlocksWindow { get; set; }
        public string MinSignedPerWindow { get; set; }
        public string DowntimeJailDuration { get; set; }
        public string SlashFractionDoubleSign { get; set; }
        public string SlashFractionDowntime { get; set; }

        internal Task<Result<SlashingParams>> Get()
        {
            return _client.GetResult(
                "/cosmos/slashing/v1beta1/params",
                new {Params = new SlashingParams()},
                data => new Result<SlashingParams> {Value = data.Params});
        }
    }
}