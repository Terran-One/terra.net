using System;
using CardanoBech32;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Terra.Sdk.Lcd
{
    public static class Global
    {
        internal static readonly Lazy<CardanoBech32Wrapper> Bech32 = new Lazy<CardanoBech32Wrapper>(() => new CardanoBech32Wrapper());

        internal static readonly JsonSerializerSettings JsonSerializerSettings = new JsonSerializerSettings
        {
            ContractResolver = new DefaultContractResolver
            {
                NamingStrategy = new SnakeCaseNamingStrategy()
            }
        };
    }
}