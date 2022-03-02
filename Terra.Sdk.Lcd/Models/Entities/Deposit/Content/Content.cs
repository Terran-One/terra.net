using JsonSubTypes;
using Newtonsoft.Json;

namespace Terra.Sdk.Lcd.Models.Entities.Deposit.Content
{
    [JsonConverter(typeof(JsonSubtypes), "@type")]
    [JsonSubtypes.KnownSubType(typeof(TextProposal), "/cosmos.gov.v1beta1.TextProposal")]
    [JsonSubtypes.KnownSubType(typeof(CommunityPoolSpendProposal), "/cosmos.gov.v1beta1.CommunityPoolSpendProposal")]
    [JsonSubtypes.KnownSubType(typeof(ParameterChangeProposal), "/cosmos.gov.v1beta1.ParameterChangeProposal")]
    [JsonSubtypes.KnownSubType(typeof(SoftwareUpgradeProposal), "/cosmos.gov.v1beta1.SoftwareUpgradeProposal")]
    [JsonSubtypes.KnownSubType(typeof(CancelSoftwareUpgradeProposal), "/cosmos.gov.v1beta1.CancelSoftwareUpgradeProposal")]
    public class Content
    {
        [JsonProperty("@type")]
        public string Type { get; set; }
    }
}
