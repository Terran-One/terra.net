using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Terra.Sdk.Lcd
{
    public static class Global
    {
        internal static readonly JsonSerializerSettings JsonSerializerSettings = new JsonSerializerSettings
        {
            ContractResolver = new DefaultContractResolver
            {
                NamingStrategy = new SnakeCaseNamingStrategy()
            }
        };
    }
}
