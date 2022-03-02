using System.Linq;
using System.Threading.Tasks;
using JsonSubTypes;
using Newtonsoft.Json;
using Terra.Sdk.Lcd.Extensions;

namespace Terra.Sdk.Lcd.Models.Entities.Allowance
{
    [JsonConverter(typeof(JsonSubtypes), "@type")]
    [JsonSubtypes.KnownSubType(typeof(BasicAllowance), "/cosmos.feegrant.v1beta1.BasicAllowance")]
    [JsonSubtypes.KnownSubType(typeof(PeriodicAllowance), "/cosmos.feegrant.v1beta1.PeriodicAllowance")]
    [JsonSubtypes.KnownSubType(typeof(AllowedMsgAllowance), "/cosmos.feegrant.v1beta1.AllowedMsgAllowance")]
    public class Allowance
    {
        private readonly LcdClient _client;

        [JsonProperty("@type")]
        public string Type { get; set; }

        public string Granter { get; set; }
        public string Grantee { get; set; }

        /// <remarks>
        ///  For serialization.
        /// </remarks>
        public Allowance()
        {
        }

        internal Allowance(LcdClient lcdClient)
        {
            _client = lcdClient;
        }

        internal Task<Result<Allowance>> Get(string granter, string grantee)
        {
            return _client.GetResult(
                $"/cosmos/feegrant/v1beta1/allowance/{granter}/{grantee}",
                new
                {
                    allowance = new
                    {
                        granter = "",
                        grantee = "",
                        allowance = new Allowance()
                    }
                },
                data => new Result<Allowance>
                {
                    Value = data.allowance.allowance.WithGrantInfo(data.allowance.granter, data.allowance.grantee)
                });
        }

        internal Task<PaginatedResult<Allowance>> GetAll(string grantee, string paginationKey = null, int? pageNumber = null, bool? getTotalCount = null, bool? isDescending = null)
        {
            return _client.GetPaginatedResult(
                $"/cosmos/feegrant/v1beta1/allowances/{grantee}",
                new
                {
                    Allowances = new[]
                    {
                        new
                        {
                            granter = "",
                            grantee = "",
                            allowance = new Allowance()
                        }
                    },
                    Pagination = new Pagination()
                },
                data => data.Pagination.BuildResult(data.Allowances.Select(a => a.allowance.WithGrantInfo(a.granter, a.grantee)).ToList(), pageNumber),
                paginationKey, pageNumber, getTotalCount, isDescending);
        }

        private Allowance WithGrantInfo(string granter, string grantee)
        {
            Granter = granter;
            Grantee = grantee;
            return this;
        }
    }
}
