using JsonSubTypes;
using Newtonsoft.Json;

namespace Terra.Sdk.Lcd.Models.Entities.Authorization
{
    [JsonConverter(typeof(JsonSubtypes), "@type")]
    [JsonSubtypes.KnownSubType(typeof(SendAuthorization), "/cosmos.bank.v1beta1.SendAuthorization")]
    [JsonSubtypes.KnownSubType(typeof(GenericAuthorization), "/cosmos.authz.v1beta1.GenericAuthorization")]
    [JsonSubtypes.KnownSubType(typeof(StakeAuthorization), "/cosmos.staking.v1beta1.StakeAuthorization")]
    public class Authorization
    {
        [JsonProperty("@type")] public string Type { get; set; }
    }
}
