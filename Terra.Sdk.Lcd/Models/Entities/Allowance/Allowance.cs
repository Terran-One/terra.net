using System.Linq;
using System.Threading.Tasks;
using JsonSubTypes;
using Newtonsoft.Json;

namespace Terra.Sdk.Lcd.Models.Entities.Allowance
{
    [JsonConverter(typeof(JsonSubtypes), "@type")]
    [JsonSubtypes.KnownSubType(typeof(BasicAllowance), "/cosmos.feegrant.v1beta1.BasicAllowance")]
    [JsonSubtypes.KnownSubType(typeof(PeriodicAllowance), "/cosmos.feegrant.v1beta1.PeriodicAllowance")]
    [JsonSubtypes.KnownSubType(typeof(AllowedMsgAllowance), "/cosmos.feegrant.v1beta1.AllowedMsgAllowance")]
    public class Allowance
    {
        private readonly LcdClient _lcdClient;

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
            _lcdClient = lcdClient;
        }

        internal async Task<Result<Allowance>> Get(string granter, string grantee)
        {
            var response = await _lcdClient.HttpClient.GetAsync($"/cosmos/feegrant/v1beta1/allowance/{granter}/{grantee}");
            if (!response.IsSuccessStatusCode)
                return new Result<Allowance> {  Error = $"Fetch failed: {response.ReasonPhrase}" };

            var json = JsonConvert.DeserializeAnonymousType(
                await response.Content.ReadAsStringAsync(),
                new
                {
                    allowance = new
                    {
                        granter = "",
                        grantee = "",
                        allowance = new Allowance()
                    }
                },
                _lcdClient.JsonSerializerSettings);

            return new Result<Allowance>
            {
                Value = json.allowance.allowance.WithGrantInfo(json.allowance.granter, json.allowance.grantee)
            };
        }

        internal async Task<PaginatedResult<Allowance>> GetAll(string grantee, string paginationKey = null, int? pageNumber = null, bool? getTotalCount = null, bool? isDescending = null)
        {
            var response = await _lcdClient.HttpClient.GetAsync($"/cosmos/feegrant/v1beta1/allowances/{grantee}{_lcdClient.GetPaginationQueryString(paginationKey, pageNumber, getTotalCount, isDescending)}");
            if (!response.IsSuccessStatusCode)
                return new PaginatedResult<Allowance> {  Error = $"Fetch failed: {response.ReasonPhrase}" };

            var json = JsonConvert.DeserializeAnonymousType(
                await response.Content.ReadAsStringAsync(),
                new
                {
                    allowances = new[]
                    {
                        new
                        {
                            granter = "",
                            grantee = "",
                            allowance = new Allowance()
                        }
                    },
                    pagination = new { next_key = "", total = 0 }
                },
                _lcdClient.JsonSerializerSettings);

            return new PaginatedResult<Allowance>
            {
                Value = json.allowances.Select(a => a.allowance.WithGrantInfo(a.granter, a.grantee)).ToList(),
                TotalCount = json.pagination?.total,
                NextPageKey = json.pagination?.next_key,
                NextPageNumber = pageNumber + 1
            };
        }

        private Allowance WithGrantInfo(string granter, string grantee)
        {
            Granter = granter;
            Grantee = grantee;
            return this;
        }
    }
}
