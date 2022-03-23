using System.Text;
using Google.Apis.Bigquery.v2.Data;
using Google.Cloud.BigQuery.V2;
using Terra.BigQuery.Roslyn;

namespace Terra.BigQuery.Etl;

public class NestedField
{
    private static readonly IDictionary<string, NestedField> Cache = new Dictionary<string, NestedField>();

    public static NestedField Create(Type type)
    {
        if (type == null)
            return null;

        if (Cache.TryGetValue(type.Name, out var nestedField))
            return nestedField;

        // Build schema
        var rowGenerators = new Dictionary<string, string>();
        nestedField = new NestedField {Schema = new TableSchema {Fields = new List<TableFieldSchema>()}};
        Populate(nestedField.Schema, type, rowGenerators);

        // Build row generation code
        var cSharpCode = new StringBuilder();
        cSharpCode.AppendLine("using System.Collections.Generic;");
        cSharpCode.AppendLine("using System.Linq;");
        cSharpCode.AppendLine("using Google.Cloud.BigQuery.V2;");
        cSharpCode.AppendLine("using Newtonsoft.Json;");
        cSharpCode.AppendLine("using Terra.BigQuery.Roslyn;");
        cSharpCode.AppendLine(BuildRowGeneratorCSharpCode(type));

        foreach (var rowGenerator in (IDictionary<string, string>) rowGenerators)
            cSharpCode.AppendLine(rowGenerator.Value);

        nestedField.GeneratedCode = cSharpCode.ToString();
        Console.WriteLine(nestedField.GeneratedCode);

        var assembly = RoslynHelpers.LoadIntoCurrentAssembly(nestedField.GeneratedCode);
        if (assembly == null)
        {
            Console.WriteLine(nestedField.GeneratedCode);
            nestedField.Success = false;
            return nestedField;
        }

        nestedField._rowGenerator = (IRowGenerator)Activator.CreateInstance(assembly.GetType($"{type.Name}RowGenerator"));

        // Nested field successfully built
        nestedField.Success = true;
        Cache[type.Name] = nestedField;
        return nestedField;
    }

    public bool Success { get; private set; }
    public TableSchema Schema { get; private init; }
    public BigQueryInsertRow BuildInsertRow(object value) => value == null ? null : _rowGenerator.GenerateRow(value);

    internal string GeneratedCode { get; private set; }
    private IRowGenerator _rowGenerator;

    private static void Populate(TableSchema schema, Type type, IDictionary<string, string> rowGenerators)
    {
        foreach (var prop in type.GetSerializableProperties())
        {
            if (prop.PropertyType == typeof(object))
            {
                AddPrimitive(schema, prop.Name, typeof(string), false);
            }
            else if (prop.PropertyType.IsCollectionOfPrimitive())
            {
                AddPrimitive(schema, prop.Name, prop.PropertyType, true);
            }
            else if (prop.PropertyType.IsPrimitive())
            {
                AddPrimitive(schema, prop.Name, prop.PropertyType, false);
            }
            else if (prop.PropertyType.IsEnum)
            {
                AddEnum(schema, prop.Name, prop.PropertyType);
            }
            else if (prop.PropertyType.IsCollection())
            {
                AddRecord(schema, prop.Name, prop.PropertyType, true, rowGenerators);
            }
            else
            {
                AddRecord(schema, prop.Name, prop.PropertyType, false, rowGenerators);
            }
        }
    }

    private static void AddRecord(TableSchema schema, string name, Type type, bool isList, IDictionary<string, string> rowGenerators)
    {
        if (isList)
            type = type.GetListElementType();

        var nestedSchema = new TableSchema {Fields = new List<TableFieldSchema>()};

        if (!rowGenerators.ContainsKey(type.Name))
            rowGenerators.Add(type.Name, BuildRowGeneratorCSharpCode(type));

        schema.Fields.Add(new TableFieldSchema
        {
            Name = name,
            Type = "RECORD",
            Mode = isList ? "REPEATED" : "NULLABLE",
            Fields = nestedSchema.Fields
        });

        Populate(nestedSchema, type, rowGenerators);
    }

    private static string BuildRowGeneratorCSharpCode(Type type)
    {
        return $@"
public class {type.Name}RowGenerator : IRowGenerator
{{
    public BigQueryInsertRow GenerateRow(object value)
    {{
        var msg = ({type.FullName.Replace('+', '.')})value;
        return new BigQueryInsertRow
        {{
            {string.Join(",\n            ", type.GetSerializableProperties().Select(prop =>
            {
                if (prop.PropertyType == typeof(object))
                    return $"{{ \"{prop.Name}\", JsonConvert.SerializeObject((object)msg.{prop.Name}, Formatting.Indented) }}";

                if (prop.PropertyType == typeof(decimal))
                    return $"{{ \"{prop.Name}\", BigQueryNumeric.FromDecimal(msg.{prop.Name}, LossOfPrecisionHandling.Truncate) }}";

                if (prop.PropertyType.IsPrimitive() || prop.PropertyType.IsCollectionOfPrimitive())
                    return $"{{ \"{prop.Name}\", msg.{prop.Name} }}";

                if (prop.PropertyType.IsEnum)
                    return $"{{ \"{prop.Name}\", (int)msg.{prop.Name} }}";

                if (prop.PropertyType.IsCollection())
                    return $"{{ \"{prop.Name}\", msg.{prop.Name}.Select(x => new {prop.PropertyType.GetListElementType().Name}RowGenerator().GenerateRow(x)).ToList() }}";

                if (prop.PropertyType.IsCollection())
                    return $"{{ \"{prop.Name}\", msg.{prop.Name}.Select(x => new {prop.PropertyType.GetListElementType().Name}RowGenerator().GenerateRow(x)).ToList() }}";

                return $"{{ \"{prop.Name}\", new {prop.PropertyType.Name}RowGenerator().GenerateRow(msg) }}";
            }))}
        }};
    }}   
}}";
    }

    private static void AddPrimitive(TableSchema schema, string name, Type type, bool isList)
    {
        if (isList)
            type = type.GetListElementType();

        var typeName = type.IsListOfBytes() ? "BYTES" : TypeExtensions.Primitives[type.Name];
        schema.Fields.Add(new TableFieldSchema
        {
            Name = name,
            Type = typeName,
            Mode = isList ? "REPEATED" : "NULLABLE"
        });
    }

    private static void AddEnum(TableSchema schema, string name, Type type)
    {
        schema.Fields.Add(new TableFieldSchema
        {
            Name = name,
            Type = "INTEGER",
            Mode = "NULLABLE"
        });
    }
}
