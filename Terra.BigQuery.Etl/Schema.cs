using System.Reflection;
using Google.Apis.Bigquery.v2.Data;

namespace Terra.BigQuery.Etl;

public static class Schema
{
    public static TableSchema Create<T>()
    {
        var schema = new TableSchema {Fields = new List<TableFieldSchema>()};
        schema.Populate(typeof(T));
        return schema;
    }

    private static void Populate(this TableSchema schema, Type type)
    {
        foreach (var prop in type.GetSerializableProperties())
        {
            if (prop.PropertyType.IsListOfPrimitive())
            {
                schema.AddPrimitive(prop.Name, prop.PropertyType, true);
            }
            else if (prop.PropertyType.IsPrimitive())
            {
                schema.AddPrimitive(prop.Name, prop.PropertyType, false);
            }
            else if (prop.PropertyType.IsList())
            {
                schema.AddBqRecord(prop.Name, prop.PropertyType, true);
            }
            else
            {
                schema.AddBqRecord(prop.Name, prop.PropertyType, false);
            }
        }
    }

    private static void AddBqRecord(this TableSchema schema, string name, Type type, bool isList)
    {
        if (isList)
            type = type.GetListElementType();

        var nestedSchema = new TableSchema {Fields = new List<TableFieldSchema>()};
        schema.Fields.Add(new TableFieldSchema {Name = name, Type = "RECORD", Mode = isList ? "REPEATED" : "NULLABLE", Fields = nestedSchema.Fields});
        nestedSchema.Populate(type);
    }

    private static readonly IDictionary<string, string> Primitives = new Dictionary<string, string>
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

    private static void AddPrimitive(this TableSchema schema, string name, Type type, bool isList)
    {
        if (isList)
            type = type.GetListElementType();

        var typeName = type.IsListOfBytes() ? "BYTES" : Primitives[type.Name];
        schema.Fields.Add(new TableFieldSchema {Name = name, Type = typeName, Mode = isList ? "REPEATED" : "NULLABLE"});
    }

    private static IEnumerable<PropertyInfo> GetSerializableProperties(this Type type)
    {
        return type.GetProperties(BindingFlags.Instance | BindingFlags.Public).Where(p => !p.GetCustomAttributes(typeof(Newtonsoft.Json.JsonIgnoreAttribute)).Any());
    }

    private static bool IsPrimitive(this Type type)
    {
        return Primitives.ContainsKey(type.Name) || type.IsListOfBytes();
    }

    private static bool IsListOfBytes(this Type type)
    {
        return type == typeof(byte[]) || type.IsList() && type.GetGenericArguments().SingleOrDefault() == typeof(byte);
    }

    private static bool IsListOfPrimitive(this Type type)
    {
        if (type.IsListOfBytes()) // special case
            return false;

        return type.GetListElementType()?.IsPrimitive() == true;
    }

    private static Type GetListElementType(this Type type)
    {
        if (type.IsArray)
            return type.GetElementType(); // Not supporting arrays of arrays for now

        if (type.IsList())
            return type.GetGenericArguments().Single();

        return null;
    }

    private static bool IsList(this Type type)
    {
        return type.Name == "List`1";
    }

}
