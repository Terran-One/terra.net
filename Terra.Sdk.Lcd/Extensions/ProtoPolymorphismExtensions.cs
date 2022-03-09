using System;
using System.Collections.Generic;
using System.Linq;
using JsonSubTypes;
using Terra.Sdk.Lcd.Models;

namespace Terra.Sdk.Lcd.Extensions
{
    public static class ProtoPolymorphismExtensions
    {
        private static readonly IDictionary<Type, IDictionary<string, Type>> SubtypeMapsCache = new Dictionary<Type, IDictionary<string, Type>>();

        public static IDictionary<string, Type> GetJsonSubtypeMap(this Type type)
        {
            return type.GetCustomAttributes(typeof(JsonSubtypes.KnownSubTypeAttribute), false)
                .Cast<JsonSubtypes.KnownSubTypeAttribute>()
                .Select(attr => Tuple.Create(attr.AssociatedValue.ToString(), attr.SubType))
                .ToDictionary(t => t.Item1, t => t.Item2);
        }

        public static T Decode<T>(this Any value)
        {
            if (!SubtypeMapsCache.TryGetValue(typeof(T), out var subtypeMap))
            {
                subtypeMap = typeof(T).GetJsonSubtypeMap();
                SubtypeMapsCache.Add(typeof(T), subtypeMap);
            }

            var type = subtypeMap[value.TypeUrl];
            var decoded = value.Value.DecodeProto(type);
            return (T)decoded;
        }
    }
}
