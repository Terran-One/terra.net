using System.Threading.Tasks;
using JsonSubTypes;
using Newtonsoft.Json;
using Terra.Sdk.Lcd.Extensions;

namespace Terra.Sdk.Lcd.Models.Entities.Account
{
    [JsonConverter(typeof(JsonSubtypes), "@type")]
    [JsonSubtypes.KnownSubType(typeof(BaseAccount), "/cosmos.auth.v1beta1.BaseAccount")]
    [JsonSubtypes.KnownSubType(typeof(LazyGradedVestingAccount), "/terra.vesting.v1beta1.LazyGradedVestingAccount")]
    public class Account
    {
        private readonly LcdClient _lcdClient;

        [JsonProperty("@type")]
        public string Type { get; set; }

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

        internal Task<Result<Account>> Get(string address)
        {
            return _lcdClient.GetResult(
                $"/cosmos/auth/v1beta1/accounts/{address}",
                new { Account = new Account() },
                data => new Result<Account> { Value = data.Account });
        }
    }
}
