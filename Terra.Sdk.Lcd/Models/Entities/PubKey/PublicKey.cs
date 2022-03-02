using JsonSubTypes;
using Newtonsoft.Json;

namespace Terra.Sdk.Lcd.Models.Entities.PubKey
{
    [JsonConverter(typeof(JsonSubtypes), "@type")]
    [JsonSubtypes.KnownSubType(typeof(SimplePublicKey), "/cosmos.crypto.secp256k1.PubKey")]
    [JsonSubtypes.KnownSubType(typeof(LegacyPublicKey), "/cosmos.crypto.multisig.LegacyAminoPubKey")]
    [JsonSubtypes.KnownSubType(typeof(ValConsPublicKey), "/cosmos.crypto.ed25519.PubKey")]
    public class PublicKey
    {
        [JsonProperty("@type")]
        public string Type { get; set; }
    }
}
