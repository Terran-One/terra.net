using System.Security.Cryptography;
using System.Text;
using CardanoBech32;

namespace Terra.Sdk.Lcd.Extensions
{
    internal static class StringExtensions
    {
        internal static string GetSha256Hash(this string rawData)
        {
            using (var sha256Hash = SHA256.Create())
            {
                var bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Convert byte array to a string
                var builder = new StringBuilder();
                foreach (var b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString().ToUpperInvariant();
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
    }
}
