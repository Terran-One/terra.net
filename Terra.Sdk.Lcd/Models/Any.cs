using System;
using System.Collections.Generic;
using System.Linq;
using JsonSubTypes;
using ProtoBuf;
using Terra.Sdk.Lcd.Extensions;

namespace Terra.Sdk.Lcd.Models
{
    /// <summary>
    /// Used for serializing fields of type google.protobuf.Any.
    /// </summary>
    /// <remarks>
    /// <see cref="Value"/> should be set to
    /// </remarks>
    [ProtoContract]
    public sealed class Any
    {
        private static readonly IDictionary<Type, IDictionary<string, Type>> SubtypeMapsCache = new Dictionary<Type, IDictionary<string, Type>>();

        /// <summary>
        /// Taken from the `@type` field.
        /// </summary>
        [ProtoMember(1, Name = "type_url")]
        public string TypeUrl { get; set; }

        /// <summary>
        /// The base64-encoded protobuf value.
        /// </summary>
        /// <remarks>
        /// You can use <see cref="Terra.Sdk.Lcd.Extensions.ObjectExtensions.EncodeProto{T}"/>
        /// to generate this value.
        /// </remarks>
        [ProtoMember(2, Name = "value")]
        public byte[] Value { get;set; }

        /// <summary>
        /// Decodes the instance into a subtype of the given type.
        /// </summary>
        internal T Decode<T>()
        {
            var type = typeof(T);
            if (!SubtypeMapsCache.TryGetValue(type, out var subtypeMap))
            {
                subtypeMap = type.GetCustomAttributes(typeof(JsonSubtypes.KnownSubTypeAttribute), false)
                                 .Cast<JsonSubtypes.KnownSubTypeAttribute>()
                                 .Select(attr => Tuple.Create(attr.AssociatedValue.ToString(), attr.SubType))
                                 .ToDictionary(t => t.Item1, t => t.Item2);
                SubtypeMapsCache.Add(type, subtypeMap);
            }

            var subtype = subtypeMap[TypeUrl];
            var decoded = Value.DecodeProto(subtype);
            return (T)decoded;
        }
    }
}
