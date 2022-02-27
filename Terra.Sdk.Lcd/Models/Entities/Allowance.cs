using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JsonSubTypes;
using Newtonsoft.Json;

namespace Terra.Sdk.Lcd.Models.Entities
{
    [JsonConverter(typeof(JsonSubtypes), "@type")]
    [JsonSubtypes.KnownSubType(typeof(BasicAllowance), "/cosmos.feegrant.v1beta1.BasicAllowance")]
    [JsonSubtypes.KnownSubType(typeof(PeriodicAllowance), "/cosmos.feegrant.v1beta1.PeriodicAllowance")]
    [JsonSubtypes.KnownSubType(typeof(AllowedMsgAllowance), "/cosmos.feegrant.v1beta1.AllowedMsgAllowance")]
    public class Allowance
    {
        private readonly LcdClient _lcdClient;

        public string Granter { get; set; }
        public string Grantee { get; set; }

        /// <remarks>
        ///  For serialization.
        /// </remarks>
        public Allowance()
        {
        }

        public Allowance(LcdClient lcdClient)
        {
            _lcdClient = lcdClient;
        }

        public async Task<Result<Allowance>> Get(string granter, string grantee)
        {
            var response = await _lcdClient.HttpClient.GetAsync($"/cosmos/feegrant/v1beta1/allowance/{granter}/{grantee}");
            if (!response.IsSuccessStatusCode)
                return new Result<Allowance> {  Error = $"Fetch failed: {response.ReasonPhrase}" };

            var json = JsonConvert.DeserializeAnonymousType(await response.Content.ReadAsStringAsync(), new
            {
                allowance = new
                {
                    granter = "",
                    grantee = "",
                    allowance = new Allowance()
                }
            });

            return new Result<Allowance>
            {
                Value = json.allowance.allowance.WithGrantInfo(json.allowance.granter, json.allowance.grantee)
            };
        }

        public async Task<PaginatedResult<Allowance>> GetAll(string grantee, string paginationKey = null, int? pageNumber = null, bool? getTotalCount = null, bool? isDescending = null)
        {
            var response = await _lcdClient.HttpClient.GetAsync($"/cosmos/feegrant/v1beta1/allowances/{grantee}{_lcdClient.GetPaginationQueryString(paginationKey, pageNumber, getTotalCount, isDescending)}");
            if (!response.IsSuccessStatusCode)
                return new PaginatedResult<Allowance> {  Error = $"Fetch failed: {response.ReasonPhrase}" };

            var json = JsonConvert.DeserializeAnonymousType(await response.Content.ReadAsStringAsync(), new
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
            });

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

    public class BasicAllowance : Allowance
    {
        [JsonProperty("spend_limit")]
        public List<Coin> SpendLimit { get; set; }

        [JsonProperty("expiration")]
        public string Expiration { get; set; }
    }

    public class PeriodicAllowance : Allowance
    {
        [JsonProperty("basic")]
        public BasicAllowance Basic { get; set; }

        [JsonProperty("period")]
        public string Period { get; set; }

        [JsonProperty("period_spend_limit")]
        public List<Coin> PeriodSpendLimit { get; set; }

        [JsonProperty("period_can_spend")]
        public List<Coin> PeriodCanSpend { get; set; }

        [JsonProperty("period_reset")]
        public string PeriodReset { get; set; }
    }

    public class AllowedMsgAllowance : Allowance
    {
        [JsonProperty("allowance")]
        public Allowance Allowance { get; set; }

        [JsonProperty("allowed_messages")]
        public List<string> AllowedMessages { get; set; }
    }
}
