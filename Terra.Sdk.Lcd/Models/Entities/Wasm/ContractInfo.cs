using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Terra.Sdk.Lcd.Extensions;

namespace Terra.Sdk.Lcd.Models.Entities.Wasm
{
    public class ContractInfo
    {
        private readonly LcdClient _client;

        /// <remarks>
        /// For serialization.
        /// </remarks>
        public ContractInfo()
        {
        }

        internal ContractInfo(LcdClient client)
        {
            _client = client;
        }

        public string CodeId { get; set; }
        public string Address { get; set; }
        public string Creator { get; set; }
        public string Admin { get; set; }
        public dynamic InitMsg { get; set; }

        internal Task<Result<ContractInfo>> Get(string contractAddress)
        {
            return _client.GetResult(
                $"/terra/wasm/v1beta1/contracts/{contractAddress}",
                new {ContractInfo = new ContractInfo()},
                data => new Result<ContractInfo> {Value = data.ContractInfo});
        }
    }
}
