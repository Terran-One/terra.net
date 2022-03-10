using System;
using JsonSubTypes;
using Newtonsoft.Json;
using ProtoBuf;
using Terra.Sdk.Lcd.Extensions;

namespace Terra.Sdk.Lcd.Models.Entities.Deposit.Content
{
    [JsonConverter(typeof(JsonSubtypes), "@type")]
    [JsonSubtypes.KnownSubType(typeof(TextProposal), "/cosmos.gov.v1beta1.TextProposal")]
    [JsonSubtypes.KnownSubType(typeof(CommunityPoolSpendProposal), "/cosmos.distribution.v1beta1.CommunityPoolSpendProposal")]
    [JsonSubtypes.KnownSubType(typeof(ParameterChangeProposal), "/cosmos.params.v1beta1.ParameterChangeProposal")]
    [JsonSubtypes.KnownSubType(typeof(SoftwareUpgradeProposal), "/cosmos.upgrade.v1beta1.SoftwareUpgradeProposal")]
    [JsonSubtypes.KnownSubType(typeof(CancelSoftwareUpgradeProposal), "/cosmos.upgrade.v1beta1.CancelSoftwareUpgradeProposal")]
    public abstract class Content
    {
        protected Content()
        {
            TypeUrl = typeof(Content).GetTypeToUrlMap()[Type.Name];
        }

        [JsonProperty("@type")]
        [ProtoMember(1, Name = "type")]
        public string TypeUrl { get; set; }
        protected abstract Type Type { get; }
    }
}
