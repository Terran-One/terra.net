using System.Text;
using Terra.BigQuery.Etl.Utils;
using Terra.BigQuery.Roslyn;
using Terra.Sdk.Lcd.Models.Entities.Tx.Msg;

namespace Terra.BigQuery.Etl;

public static class MessageDeserializer
{
    public static IMessageDeserializer Get()
    {
        var cSharpCode = new StringBuilder();
        cSharpCode.AppendLine("using System;");
        cSharpCode.AppendLine("using System.Collections.Generic;");
        cSharpCode.AppendLine("using System.Linq;");
        cSharpCode.AppendLine("using Google.Cloud.BigQuery.V2;");
        cSharpCode.AppendLine("using Newtonsoft.Json;");
        cSharpCode.AppendLine("using Newtonsoft.Json.Serialization;");
        cSharpCode.AppendLine("using Newtonsoft.Json.Linq;");
        cSharpCode.AppendLine("using Terra.BigQuery.Roslyn;");
        cSharpCode.AppendLine("using Terra.Sdk.Lcd.Models.Entities.Tx.Msg;");

        cSharpCode.AppendLine(@"
public class Deserializer : IMessageDeserializer
{
    public Tuple<Msg, Type, string> Deserialize(string type, JObject json)
    {
        return type switch
        {");

        foreach (var type in typeof(Msg).Assembly.GetTypes().Where(type => type.IsSubclassOf(typeof(Msg))))
            cSharpCode.AppendLine(@$"            ""{type.Name}"" => Tuple.Create((Msg)json.ToObject<{type.FullName}>(JsonSerializer.Create(new JsonSerializerSettings {{ContractResolver = new DefaultContractResolver {{NamingStrategy = new SnakeCaseNamingStrategy()}}}})), typeof({type.FullName}), type),");

        cSharpCode.AppendLine(@"
            _ => Tuple.Create((Msg)null, (Type)null, type)
        };
    }
}");

        var assembly = RoslynHelpers.LoadIntoCurrentAssembly(cSharpCode.ToString());
        return (IMessageDeserializer)Activator.CreateInstance(assembly.GetType("Deserializer"));
    }
}
