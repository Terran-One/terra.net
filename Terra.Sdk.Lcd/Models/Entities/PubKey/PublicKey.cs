using System;
using System.Collections.Generic;
using System.Linq;
using JsonSubTypes;
using Newtonsoft.Json;
using ProtoBuf;
using Terra.Sdk.Lcd.Extensions;

namespace Terra.Sdk.Lcd.Models.Entities.PubKey
{
    [JsonConverter(typeof(JsonSubtypes), "@type")]
    [JsonSubtypes.KnownSubType(typeof(SimplePublicKey), "/cosmos.crypto.secp256k1.PubKey")]
    [JsonSubtypes.KnownSubType(typeof(LegacyAminoMultisigPublicKey), "/cosmos.crypto.multisig.LegacyAminoPubKey")]
    [JsonSubtypes.KnownSubType(typeof(ValConsPublicKey), "/cosmos.crypto.ed25519.PubKey")]

    [ProtoContract]
    [ProtoInclude(1, typeof(SimplePublicKey))]
    [ProtoInclude(2, typeof(LegacyAminoMultisigPublicKey))]
    [ProtoInclude(3, typeof(ValConsPublicKey))]
    public abstract class PublicKey
    {
        internal static readonly Lazy<IDictionary<string, Type>> SubtypeMap = new Lazy<IDictionary<string, Type>>(() =>
            typeof(PublicKey).GetCustomAttributes(typeof(JsonSubtypes.KnownSubTypeAttribute), false)
                             .Cast<JsonSubtypes.KnownSubTypeAttribute>()
                             .Select(attr => Tuple.Create(attr.AssociatedValue.ToString(), attr.SubType))
                             .ToDictionary(t => t.Item1, t => t.Item2));

        [JsonProperty("@type")]
        public string Type { get; set; }

        public abstract string Key { get; set; }

        public byte[] RawAddress => Convert.FromBase64String(Key).GetSha256Hash().Take(20).Select(b => (byte) b).ToArray();
        public string Address => RawAddress.ToHexString().ConvertToBech32AddressFromHex("terravalcons");
    }
}
