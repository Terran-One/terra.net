using System.Threading.Tasks;
using Terra.Sdk.Lcd.Extensions;

namespace Terra.Sdk.Lcd.Models.Entities.Wasm
{
    public class CodeInfo
    {
        private readonly LcdClient _client;

        /// <remarks>
        /// For serialization.
        /// </remarks>
        public CodeInfo()
        {
        }

        internal CodeInfo(LcdClient client)
        {
            _client = client;
        }

        public string CodeId { get; set; }
        public string CodeHash { get; set; }
        public string Creator { get; set; }

        internal Task<Result<CodeInfo>> Get(long codeId)
        {
            return _client.GetResult(
                $"/terra/wasm/v1beta1/codes/{codeId}",
                new {CodeInfo = new CodeInfo()},
                data => new Result<CodeInfo> {Value = data.CodeInfo});
        }
    }
}