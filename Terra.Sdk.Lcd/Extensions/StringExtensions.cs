using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Terra.Sdk.Lcd.Extensions
{
    internal static class StringExtensions
    {
        /// <remarks>
        /// Source: https://stackoverflow.com/a/51428508
        /// </remarks>
        internal static bool TryParseJson<T>(this string @this, out T result)
        {
            var success = true;
            var settings = new JsonSerializerSettings
            {
                Error = (sender, args) =>
                {
                    success = false;
                    args.ErrorContext.Handled = true;
                },
                MissingMemberHandling = MissingMemberHandling.Error,
                ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new SnakeCaseNamingStrategy()
                }
            };
            result = JsonConvert.DeserializeObject<T>(@this, settings);
            return success;
        }
    }
}
