using System.Collections.Generic;
using System.Threading.Tasks;
using JsonSubTypes;
using Newtonsoft.Json;

namespace Terra.Sdk.Lcd.Models.Entities
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

        internal Account(LcdClient lcdClient)
        {
            _lcdClient = lcdClient;
        }

        internal async Task<Result<Account>> Get(string address)
        {
            var response = await _lcdClient.HttpClient.GetAsync($"/cosmos/auth/v1beta1/accounts/{address}");
            if (!response.IsSuccessStatusCode)
                return new Result<Account> {  Error = $"Fetch failed: {response.ReasonPhrase}" };

            var json = JsonConvert.DeserializeAnonymousType(await response.Content.ReadAsStringAsync(), new { account = new Account() });
            return new Result<Account> { Value = json.account };
        }
    }

    public class BaseAccount : Account
    {
        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("pub_key")]
        public PubKey PubKey { get; set; }

        [JsonProperty("account_number")]
        public long AccountNumber { get; set; }

        [JsonProperty("sequence")]
        public long Sequence { get; set; }
    }

    public class LazyGradedVestingAccount : Account
    {
        [JsonProperty("base_vesting_account")]
        public BaseAccount BaseVestingAccount { get; set; }

        [JsonProperty("vesting_schedules")]
        public List<VestingSchedule> VestingSchedules { get; set; }
    }

    public class VestingSchedule
    {
        [JsonProperty("denom")]
        public string Denom { get; set; }

        [JsonProperty("schedules")]
        public List<VestingScheduleEntry> Schedules { get; set; }
    }

    public class VestingScheduleEntry
    {
        [JsonProperty("start_time")]
        public long StartTime { get; set; }

        [JsonProperty("end_time")]
        public long EndTime { get; set; }

        [JsonProperty("ratio")]
        public decimal Ratio { get; set; }
    }

    [JsonConverter(typeof(JsonSubtypes), "@type")]
    [JsonSubtypes.KnownSubType(typeof(SimplePublicKey), "/cosmos.crypto.secp256k1.PubKey")]
    [JsonSubtypes.KnownSubType(typeof(LegacyPublicKey), "/cosmos.crypto.multisig.LegacyAminoPubKey")]
    [JsonSubtypes.KnownSubType(typeof(ValConsPublicKey), "/cosmos.crypto.ed25519.PubKey")]
    public class PubKey
    {
    }

    public class SimplePublicKey : PubKey
    {
        [JsonProperty("key")]
        public string Key { get; set; }
    }

    public class LegacyPublicKey : PubKey
    {
        [JsonProperty("threshold")]
        public string Threshold { get; set; }

        [JsonProperty("public_keys")]
        public List<SimplePublicKey> PublicKeys { get; set; }
    }

    public class ValConsPublicKey : PubKey
    {
        [JsonProperty("key")]
        public string Key { get; set; }
    }
}
