using Newtonsoft.Json;

namespace Terra.Sdk.Lcd.Models.Entities.Staking
{
    public class RedelegationEntry
    {
        [JsonProperty("redelegation_entry")] public RedelegationEntryDetails Details { get; set; }
        public string Balance { get; set; }
    }
}