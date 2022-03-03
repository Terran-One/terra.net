using System.Threading.Tasks;
using Terra.Sdk.Lcd.Extensions;

namespace Terra.Sdk.Lcd.Models.Entities.Ibc
{
    public class IbcTransferParams
    {
        private readonly LcdClient _client;

        /// <remarks>
        /// For serialization.
        /// </remarks>
        public IbcTransferParams()
        {
        }

        internal IbcTransferParams(LcdClient client)
        {
            _client = client;
        }

        public bool SendEnabled { get; set; }
        public bool ReceiveEnabled { get; set; }

        public Task<Result<IbcTransferParams>> Get()
        {
            return _client.GetResult(
                "/ibc/apps/transfer/v1/params",
                new { Params = new IbcTransferParams() },
                data => new Result<IbcTransferParams> { Value = data.Params });
        }
    }
}
