using System.Collections.Generic;
using System.Threading.Tasks;
using Terra.Sdk.Lcd.Extensions;

namespace Terra.Sdk.Lcd.Models.Entities.Ibc
{
    public class IbcClientParams
    {
        private readonly LcdClient _client;

        /// <remarks>
        /// For serialization.
        /// </remarks>
        public IbcClientParams()
        {
        }

        internal IbcClientParams(LcdClient client)
        {
            _client = client;
        }

        public List<string> AllowedClients { get; set; }

        internal Task<Result<IbcClientParams>> Get()
        {
            return _client.GetResult(
                "/ibc/client/v1/params",
                new {Params = new IbcClientParams()},
                data => new Result<IbcClientParams> {Value = data.Params});
        }
    }
}
