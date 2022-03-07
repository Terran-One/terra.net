using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Terra.Sdk.Lcd.Models;

namespace Terra.Sdk.Lcd.Extensions
{
    internal static class HttpExtensions
    {
        internal static async Task<string> GetErrorString(this HttpResponseMessage response) =>
            $"Fetch failed: {response.ReasonPhrase} ({await response.Content.ReadAsStringAsync()})";

        internal static async Task<Result<TEntity>> GetResult<TEntity>(this LcdClient client, string url, string additionalParams = null)
            where TEntity : new()
        {
            if (additionalParams != null)
                url = $"{url}?{additionalParams}";

            var response = await client.HttpClient.GetAsync(url);
            if (!response.IsSuccessStatusCode)
                return new Result<TEntity> { Error = await response.GetErrorString() };

            var data = JsonConvert.DeserializeObject<TEntity>(
                await response.Content.ReadAsStringAsync(),
                client.JsonSerializerSettings);
            return new Result<TEntity> { Value = data };
        }

        internal static async Task<Result<TEntity>> GetResult<TEntity, TAnonymousType>(this LcdClient client,
            string url, TAnonymousType anonymousTypeDefinition, Func<TAnonymousType, Result<TEntity>> resultBuilder,
            string additionalParams = null)
        {
            if (additionalParams != null)
                url = $"{url}?{additionalParams}";

            var response = await client.HttpClient.GetAsync(url);
            if (!response.IsSuccessStatusCode)
                return new Result<TEntity> { Error = await response.GetErrorString()};

            var data = JsonConvert.DeserializeAnonymousType(
                await response.Content.ReadAsStringAsync(),
                anonymousTypeDefinition,
                client.JsonSerializerSettings);
            return resultBuilder(data);
        }

        internal static async Task<PaginatedResult<TEntity>> GetPaginatedResult<TEntity, TAnonymousType>(this LcdClient lcdClient,
            string url, TAnonymousType anonymousTypeDefinition, Func<TAnonymousType, PaginatedResult<TEntity>> resultBuilder,
            string paginationKey, int? pageNumber, bool? getTotalCount, bool? isDescending,
            string additionalParams = null)
        {
            var paginationParams = lcdClient.GetPaginationQueryString(paginationKey, pageNumber, getTotalCount, isDescending);
            var queryString = CombineQueryStrings(paginationParams, additionalParams);

            var response = await lcdClient.HttpClient.GetAsync($"{url}{queryString}");
            if (!response.IsSuccessStatusCode)
                return new PaginatedResult<TEntity> { Error = await response.GetErrorString() };

            var data = JsonConvert.DeserializeAnonymousType(
                await response.Content.ReadAsStringAsync(),
                anonymousTypeDefinition,
                lcdClient.JsonSerializerSettings);
            return resultBuilder(data);
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
