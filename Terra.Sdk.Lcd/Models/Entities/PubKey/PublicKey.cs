using System;
using System.Linq;
using JsonSubTypes;
using Newtonsoft.Json;
using Terra.Sdk.Lcd.Extensions;

namespace Terra.Sdk.Lcd.Models.Entities.PubKey
{
    [JsonConverter(typeof(JsonSubtypes), "@type")]
    [JsonSubtypes.KnownSubType(typeof(SimplePublicKey), "/cosmos.crypto.secp256k1.PubKey")]
    [JsonSubtypes.KnownSubType(typeof(LegacyAminoMultisigPublicKey), "/cosmos.crypto.multisig.LegacyAminoPubKey")]
    [JsonSubtypes.KnownSubType(typeof(ValConsPublicKey), "/cosmos.crypto.ed25519.PubKey")]
    public abstract class PublicKey
    {
        [JsonProperty("@type")]
        public string Type { get; set; }

        public abstract string Key { get; set; }

        public byte[] RawAddress => Convert.FromBase64String(Key).GetSha256Hash().Take(20).Select(b => (byte) b).ToArray();
        public string Address => RawAddress.ToHexString().ConvertToBech32AddressFromHex("terravalcons");
    }
}
