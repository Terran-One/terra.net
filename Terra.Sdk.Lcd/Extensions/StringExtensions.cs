using System.Security.Cryptography;
using System.Text;

namespace Terra.Sdk.Lcd.Extensions
{
    public static class StringExtensions
    {
        public static string GetSha256Hash(this string rawData)
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
    }
}
