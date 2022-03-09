using System.Security.Cryptography;
using System.Text;
using CardanoBech32;

namespace Terra.Sdk.Lcd.Extensions
{
    internal static class HashingExtensions
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

        internal static string GetSha256Hash(this byte[] rawData)
        {
            using (var sha256Hash = SHA256.Create())
            {
                var bytes = sha256Hash.ComputeHash(rawData);

                // Convert byte array to a string
                var builder = new StringBuilder();
                foreach (var b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }

                return builder.ToString().ToUpperInvariant();
            }
        }

        internal static string ToHexString(this byte[] bytes)
        {
            var hex = new StringBuilder(bytes.Length * 2);
            foreach (var b in bytes)
                hex.AppendFormat("{0:x2}", b);

            return hex.ToString();
        }
    }
}
