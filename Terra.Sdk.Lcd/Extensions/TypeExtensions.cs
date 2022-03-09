using System;
using System.Collections.Generic;
using System.Linq;
using JsonSubTypes;

namespace Terra.Sdk.Lcd.Extensions
{
    public static class TypeExtensions
    {
        public static IDictionary<string, Type> GetJsonSubtypeMap(this Type type)
        {
            return type.GetCustomAttributes(typeof(JsonSubtypes.KnownSubTypeAttribute), false)
                .Cast<JsonSubtypes.KnownSubTypeAttribute>()
                .Select(attr => Tuple.Create(attr.AssociatedValue.ToString(), attr.SubType))
                .ToDictionary(t => t.Item1, t => t.Item2);
        }
    }
}
