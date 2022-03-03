using System.Threading.Tasks;
using Terra.Sdk.Lcd.Extensions;

namespace Terra.Sdk.Lcd.Models.Entities
{
    public class TreasuryParams
    {
        private readonly LcdClient _client;

        /// <remarks>
        /// For serialization.
        /// </remarks>
        public TreasuryParams()
        {
        }

        internal TreasuryParams(LcdClient client)
        {
            _client = client;
        }

        internal Task<Result<TreasuryParams>> Get()
        {
            return _client.GetResult(
                "/terra/wasm/treasury/params",
                new {Params = new TreasuryParams()},
                data => new Result<TreasuryParams> {Value = data.Params});
        }
    }
}
