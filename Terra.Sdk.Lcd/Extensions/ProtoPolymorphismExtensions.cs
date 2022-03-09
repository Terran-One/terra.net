using System;
using System.Collections.Generic;
using System.Linq;
using JsonSubTypes;

namespace Terra.Sdk.Lcd.Extensions
{
    internal static class ProtoPolymorphismExtensions
    {
        private static readonly IDictionary<string, IDictionary<string, Type>> UrlToTypeCache = new Dictionary<string, IDictionary<string, Type>>();
        private static readonly IDictionary<string, IDictionary<string, string>> TypeToUrlCache = new Dictionary<string, IDictionary<string, string>>();

        internal static IDictionary<string, Type> GetUrlToTypeMap(this Type type)
        {
            var typeName = type.Name;
            if (UrlToTypeCache.TryGetValue(typeName, out var subtypeMap))
                return subtypeMap;

            subtypeMap = type.GetCustomAttributes(typeof(JsonSubtypes.KnownSubTypeAttribute), false)
                             .Cast<JsonSubtypes.KnownSubTypeAttribute>()
                             .Select(attr => Tuple.Create(attr.AssociatedValue.ToString(), attr.SubType))
                             .ToDictionary(t => t.Item1, t => t.Item2);
            UrlToTypeCache.Add(typeName, subtypeMap);
            return subtypeMap;
        }

        internal static IDictionary<string, string> GetTypeToUrlMap(this Type type)
        {
            var typeName = type.Name;
            if (TypeToUrlCache.TryGetValue(typeName, out var subtypeMap))
                return subtypeMap;

            subtypeMap = type.GetCustomAttributes(typeof(JsonSubtypes.KnownSubTypeAttribute), false)
                             .Cast<JsonSubtypes.KnownSubTypeAttribute>()
                             .Select(attr => Tuple.Create(attr.SubType.Name, attr.AssociatedValue.ToString()))
                             .ToDictionary(t => t.Item1, t => t.Item2);
            TypeToUrlCache.Add(typeName, subtypeMap);
            return subtypeMap;
        }
    }
}
