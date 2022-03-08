using System.Security.Cryptography;
using CardanoBech32;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Terra.Sdk.Lcd.Extensions
{
    internal static class StringExtensions
    {
        internal static string GetSha256Hash(this string rawData)
        {
            using (var sha256Hash = SHA256.Create())
            {
                var wordArray = sha256Hash.ComputeHash(System.Convert.FromBase64String(rawData));
                return Helper.ConvertByteToHexString(wordArray);
            }
        }

        internal static string ConvertToBech32AddressFromHex(this string inHex, string prefix)
        {
            if (inHex == null)
                return null;

            inHex = inHex.Trim();
            var data = Helper.ConvertHexStringToByte(inHex);
            return Bech32Engine.Encode(prefix, data);
        }

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