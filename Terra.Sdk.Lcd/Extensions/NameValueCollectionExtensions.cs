using System.Collections.Specialized;
using System.Web;

namespace Terra.Sdk.Lcd.Extensions
{
    internal static class NameValueCollectionExtensions
    {
        internal static string ToQueryString(this NameValueCollection source)
        {
            if (source == null)
                return null;

            var query = HttpUtility.ParseQueryString(string.Empty);
            foreach (var key in source.AllKeys)
            {
                if (!string.IsNullOrWhiteSpace(source[key]))
                {
                    query[key] = source[key];
                }
            }

            return query.ToString();
        }
    }
}