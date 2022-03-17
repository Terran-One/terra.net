using System.Reflection;
using System.Text;
using Google.Apis.Bigquery.v2.Data;
using Google.Cloud.BigQuery.V2;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Newtonsoft.Json;
using Terra.BigQuery.Roslyn;
using Terra.Sdk.Lcd.Models.Entities.Tx.Msg;

namespace Terra.BigQuery.Etl;

public class NestedField
{
    public static NestedField Create(Type type)
    {
        // Build schema
        var rowGenerators = new Dictionary<string, string>();
        var builder = new NestedField {Schema = new TableSchema {Fields = new List<TableFieldSchema>()}};
        Populate(builder.Schema, type, rowGenerators);

        // Build row generation code
        var cSharpCode = new StringBuilder();
        cSharpCode.AppendLine("using System.Collections.Generic;");
        cSharpCode.AppendLine("using System.Linq;");
        cSharpCode.AppendLine("using Google.Cloud.BigQuery.V2;");
        cSharpCode.AppendLine("using Terra.BigQuery.Roslyn;");
        cSharpCode.AppendLine(GetRowGeneratorCSharpCode(type));

        foreach (var rowGenerator in (IDictionary<string, string>) rowGenerators)
            cSharpCode.AppendLine(rowGenerator.Value);

        builder.CSharpCode = cSharpCode.ToString();
        var assembly = LoadIntoCurrentAssembly(builder.CSharpCode);
        if (assembly == null)
        {
            builder.Success = false;
            return builder;
        }

        builder._rowGenerator = (IRowGenerator)Activator.CreateInstance(assembly.GetType($"{type.Name}RowGenerator"));
        builder.Success = true;
        return builder;
    }

    public bool Success { get; private set; }
    public TableSchema Schema { get; private init; }
    public string CSharpCode { get; private set; }
    public BigQueryInsertRow GetRow(object value) => _rowGenerator.GenerateRow(value);

    private IRowGenerator _rowGenerator;

    private static void Populate(TableSchema schema, Type type, IDictionary<string, string> rowGenerators)
    {
        foreach (var prop in type.GetSerializableProperties())
        {
            if (prop.PropertyType.IsListOfPrimitive())
            {
                AddPrimitive(schema, prop.Name, prop.PropertyType, true);
            }
            else if (prop.PropertyType.IsPrimitive())
            {
                AddPrimitive(schema, prop.Name, prop.PropertyType, false);
            }
            else if (prop.PropertyType.IsList())
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
            rowGenerators.Add(type.Name, GetRowGeneratorCSharpCode(type));

        schema.Fields.Add(new TableFieldSchema
        {
            Name = name,
            Type = "RECORD",
            Mode = isList ? "REPEATED" : "NULLABLE",
            Fields = nestedSchema.Fields
        });

        Populate(nestedSchema, type, rowGenerators);
    }

    private static string GetRowGeneratorCSharpCode(Type type)
    {
        return $@"
public class {type.Name}RowGenerator : IRowGenerator
{{
    public BigQueryInsertRow GenerateRow(object value)
    {{
        var msg = ({type.FullName.Replace('+', '.')})value;
        return new BigQueryInsertRow
        {{
            {string.Join(",\n            ", type.GetProperties(BindingFlags.Public | BindingFlags.Instance).Select(prop =>
            {
                if (prop.PropertyType == typeof(decimal))
                    return $"{{ \"{prop.Name}\", BigQueryNumeric.FromDecimal(msg.{prop.Name}, LossOfPrecisionHandling.Truncate) }}";

                if (TypeExtensions.Primitives.ContainsKey(prop.PropertyType.Name))
                    return $"{{ \"{prop.Name}\", msg.{prop.Name} }}";

                if (prop.PropertyType.IsArray || prop.PropertyType.IsList())
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

    private static Assembly LoadIntoCurrentAssembly(string cSharp)
    {
        var syntaxTree = CSharpSyntaxTree.ParseText(cSharp);

        var assemblyName = Path.GetRandomFileName();
        var mscorlibLocation = typeof(object).Assembly.Location;
        var references = new MetadataReference[]
        {
            MetadataReference.CreateFromFile(mscorlibLocation),
            MetadataReference.CreateFromFile(Path.Combine(Path.GetDirectoryName(mscorlibLocation), "System.Runtime.dll")),
            MetadataReference.CreateFromFile(Path.Combine(Directory.GetParent(mscorlibLocation).FullName, "System.Collections.dll")),
            MetadataReference.CreateFromFile(Path.Combine(Directory.GetParent(mscorlibLocation).FullName, "System.Linq.dll")),
            MetadataReference.CreateFromFile(Path.Combine(Directory.GetParent(mscorlibLocation).FullName, "netstandard.dll")),
            MetadataReference.CreateFromFile(typeof(JsonConvert).Assembly.Location),
            MetadataReference.CreateFromFile(typeof(BigQueryInsertRow).Assembly.Location),
            MetadataReference.CreateFromFile(typeof(IRowGenerator).Assembly.Location),
            MetadataReference.CreateFromFile(typeof(Msg).Assembly.Location),
        };

        var compilation = CSharpCompilation.Create(
            assemblyName,
            new[] {syntaxTree},
            references,
            new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

        using var ms = new MemoryStream();

        var result = compilation.Emit(ms);
        if (!result.Success)
        {
            var failures = result.Diagnostics.Where(diagnostic => diagnostic.IsWarningAsError || diagnostic.Severity == DiagnosticSeverity.Error);
            foreach (var diagnostic in failures)
                Console.Error.WriteLine("{0}: {1}", diagnostic.Id, diagnostic.GetMessage());

            return null;
        }

        ms.Seek(0, SeekOrigin.Begin);
        return Assembly.Load(ms.ToArray());
    }
}
