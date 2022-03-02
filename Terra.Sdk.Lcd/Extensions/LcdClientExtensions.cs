using System;
using System.Collections.Specialized;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using Terra.Sdk.Lcd.Models;

namespace Terra.Sdk.Lcd.Extensions
{
    public static class HttpClientExtensions
    {
        internal static async Task<Result<TEntity>> GetResult<TEntity, TAnonymousType>(this LcdClient lcdClient,
            string url, TAnonymousType anonymousTypeDefinition, Func<TAnonymousType, Result<TEntity>> resultBuilder)
        {
            var response = await lcdClient.HttpClient.GetAsync(url);
            if (!response.IsSuccessStatusCode)
                return new Result<TEntity> {  Error = $"Fetch failed: {response.ReasonPhrase}" };

            var data = JsonConvert.DeserializeAnonymousType(
                await response.Content.ReadAsStringAsync(),
                anonymousTypeDefinition,
                lcdClient.JsonSerializerSettings);
            return resultBuilder(data);
        }

        internal static async Task<PaginatedResult<TEntity>> GetPaginatedResult<TEntity, TAnonymousType>(this LcdClient lcdClient,
            string url, TAnonymousType anonymousTypeDefinition, Func<TAnonymousType, PaginatedResult<TEntity>> resultBuilder,
            string paginationKey, int? pageNumber, bool? getTotalCount, bool? isDescending,
            NameValueCollection additionalQueryParams = null)
        {
            var paginationParams = lcdClient.GetPaginationQueryString(paginationKey, pageNumber, getTotalCount, isDescending);
            var additionalParams = additionalQueryParams?.ToQueryString();
            var queryString = CombineQueryStrings(paginationParams, additionalParams);

            var response = await lcdClient.HttpClient.GetAsync($"{url}{queryString}");
            if (!response.IsSuccessStatusCode)
                return new PaginatedResult<TEntity> { Error = $"Fetch failed: {response.ReasonPhrase}" };

            var data = JsonConvert.DeserializeAnonymousType(
                await response.Content.ReadAsStringAsync(),
                anonymousTypeDefinition,
                lcdClient.JsonSerializerSettings);
            return resultBuilder(data);
        }

        private static string ToQueryString(this NameValueCollection source)
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

        private static string CombineQueryStrings(string a, string b)
        {
            var queryString = new StringBuilder();
            if (!string.IsNullOrWhiteSpace(b))
            {
                queryString.Append($"?{b}");
                if (!string.IsNullOrWhiteSpace(a))
                    queryString.Append(a.TrimStart('?'));
            }
            else
            {
                queryString.Append(a);
            }

            return queryString.ToString();
        }
    }
}
