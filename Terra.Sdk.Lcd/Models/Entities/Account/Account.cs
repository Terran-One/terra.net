using System.Threading.Tasks;
using JsonSubTypes;
using Newtonsoft.Json;

namespace Terra.Sdk.Lcd.Models.Entities.Account
{
    [JsonConverter(typeof(JsonSubtypes), "@type")]
    [JsonSubtypes.KnownSubType(typeof(BaseAccount), "/cosmos.auth.v1beta1.BaseAccount")]
    [JsonSubtypes.KnownSubType(typeof(LazyGradedVestingAccount), "/terra.vesting.v1beta1.LazyGradedVestingAccount")]
    public class Account
    {
        private readonly LcdClient _lcdClient;

        /// <remarks>
        ///  For serialization.
        /// </remarks>
        public Account()
        {
        }

        [JsonProperty("@type")]
        public string Type { get; set; }

        internal Account(LcdClient lcdClient)
        {
            _lcdClient = lcdClient;
        }

        internal async Task<Result<Account>> Get(string address)
        {
            var response = await _lcdClient.HttpClient.GetAsync($"/cosmos/auth/v1beta1/accounts/{address}");
            if (!response.IsSuccessStatusCode)
                return new Result<Account> {  Error = $"Fetch failed: {response.ReasonPhrase}" };

            var json = JsonConvert.DeserializeAnonymousType(
                await response.Content.ReadAsStringAsync(),
                new { Account = new Account() },
                _lcdClient.JsonSerializerSettings);
            return new Result<Account> { Value = json.Account };
        }
    }
}
