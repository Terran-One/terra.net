using Newtonsoft.Json;

namespace Terra.BigQuery.Etl.Utils;

public static class ObjectExtensions
{
    public static void Dump(this object value)
    {
        var jsonSerializerSettings = new JsonSerializerSettings {ReferenceLoopHandling = ReferenceLoopHandling.Ignore};
        var json = JsonConvert.SerializeObject(value, Formatting.Indented, jsonSerializerSettings);
        Console.WriteLine(json);
    }
}
