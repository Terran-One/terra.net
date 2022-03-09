using System;
using System.Linq;
using JsonSubTypes;
using ProtoBuf;

namespace Terra.Sdk.Lcd.Models.Entities.PubKey
{
    [ProtoContract]
    public class SimplePublicKey : PublicKey
    {
        protected override Type Type => typeof(SimplePublicKey);

        public SimplePublicKey()
        {
            var typeMap = typeof(PublicKey).GetCustomAttributes(typeof(JsonSubtypes.KnownSubTypeAttribute), false)
                .Cast<JsonSubtypes.KnownSubTypeAttribute>()
                .Select(attr => Tuple.Create(attr.SubType.Name, attr.AssociatedValue.ToString()))
                .ToDictionary(t => t.Item1, t => t.Item2);
            TypeUrl = typeMap[nameof(SimplePublicKey)];
        }

        [ProtoMember(1, Name = "key")] public override string Key { get; set; }
    }
}
