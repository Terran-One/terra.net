using System.Threading.Tasks;
using Terra.Sdk.Lcd.Extensions;

namespace Terra.Sdk.Lcd.Models.Entities.Wasm
{
    public class WasmParams
    {
        private readonly LcdClient _client;

        /// <remarks>
        /// For serialization.
        /// </remarks>
        public WasmParams()
        {
        }

        internal WasmParams(LcdClient client)
        {
            _client = client;
        }

        public string MaxContractSize { get; set; }
        public string MaxContractGas { get; set; }
        public string MaxContractMsgSize { get; set; }

        internal Task<Result<WasmParams>> Get()
        {
            return _client.GetResult(
                "/terra/wasm/v1beta1/params",
                new {Params = new WasmParams()},
                data => new Result<WasmParams> {Value = data.Params});
        }
    }
}