using System.Reflection;
using Newtonsoft.Json;

namespace Terra.BigQuery.Etl.Utils;

public static class TypeExtensions
{
    public static readonly IDictionary<string, string> Primitives = new Dictionary<string, string>
    {
        {"Boolean", "BOOLEAN"},
        {"Byte", "BYTEINT"},
        {"SByte", "BYTEINT"},
        {"Char", "TINYINT"},
        {"Decimal", "DECIMAL"},
        {"Double", "FLOAT64"},
        {"Single", "FLOAT64"},
        {"Int32", "INTEGER"},
        {"UInt32", "INTEGER"},
        {"IntPtr", "INTEGER"},
        {"UIntPtr", "INTEGER"},
        {"Int64", "INT64"},
        {"UInt64", "INT64"},
        {"Int16", "SMALLINT"},
        {"UInt16", "SMALLINT"},
        {"String", "STRING"},
        {"DateTime", "DATETIME"}
    };

    public static IEnumerable<PropertyInfo> GetSerializableProperties(this Type type)
    {
        return type.GetProperties(BindingFlags.Instance | BindingFlags.Public).Where(p => !p.GetCustomAttributes(typeof(JsonIgnoreAttribute)).Any());
    }

    public static bool IsPrimitive(this Type type)
    {
        return Primitives.ContainsKey(type.Name) || type.IsListOfBytes();
    }

    public static bool IsListOfBytes(this Type type)
    {
        return type == typeof(byte[]) || type.IsCollection() && type.GetGenericArguments().SingleOrDefault() == typeof(byte);
    }

    public static bool IsCollectionOfPrimitive(this Type type)
    {
        if (type.IsListOfBytes()) // special case
            return false;

        return type.GetListElementType()?.IsPrimitive() == true;
    }

    public static Type GetListElementType(this Type type)
    {
        if (type.IsArray)
            return type.GetElementType(); // Not supporting arrays of arrays for now

        if (type.IsCollection())
            return type.GetGenericArguments().Single();

        return null;
    }

    public static bool IsCollection(this Type type)
    {
        return type.IsArray || type.Name == "List`1";
    }
}
