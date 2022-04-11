using System.Reflection;
using Google.Cloud.BigQuery.V2;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Newtonsoft.Json;
using Terra.BigQuery.Roslyn;
using Terra.Sdk.Lcd.Models.Entities.Tx.Msg;

namespace Terra.BigQuery.Etl.Utils;

public static class RoslynHelpers
{
    public static Assembly LoadIntoCurrentAssembly(string cSharp)
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
