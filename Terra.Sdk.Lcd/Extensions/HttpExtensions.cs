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
        internal static async Task<Result<TEntity>> GetErrorResult<TEntity>(this HttpResponseMessage response) =>
            new Result<TEntity> { Error = Error.From(await response.Content.ReadAsStringAsync()) };

        internal static async Task<PaginatedResult<TEntity>> GetPaginatedErrorResult<TEntity>(this HttpResponseMessage response) =>
            new PaginatedResult<TEntity> { Error = Error.From(await response.Content.ReadAsStringAsync()) };

        internal static async Task<PaginatedGroupedResult<TEntity>> GetPaginatedGroupedErrorResult<TEntity>(this HttpResponseMessage response) =>
            new PaginatedGroupedResult<TEntity> { Error = Error.From(await response.Content.ReadAsStringAsync()) };

        internal static async Task<Result<TEntity>> GetResult<TEntity>(this LcdClient client, string url, string additionalParams = null)
            where TEntity : new()
        {
            if (additionalParams != null)
                url = $"{url}?{additionalParams}";

            var response = await client.HttpClient.GetAsync(url);
            if (!response.IsSuccessStatusCode)
                return await response.GetErrorResult<TEntity>();

            var data = JsonConvert.DeserializeObject<TEntity>(
                await response.Content.ReadAsStringAsync(),
                Global.JsonSerializerSettings);
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
                return await response.GetErrorResult<TEntity>();

            var jsonStr = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeAnonymousType(
                jsonStr,
                anonymousTypeDefinition,
                Global.JsonSerializerSettings);
            return resultBuilder(data);
        }

        internal static async Task<PaginatedResult<TEntity>> GetPaginatedResult<TEntity, TAnonymousType>(this LcdClient client,
            string url, TAnonymousType anonymousTypeDefinition, Func<TAnonymousType, PaginatedResult<TEntity>> resultBuilder,
            string paginationKey, int? pageNumber, bool? getTotalCount, bool? isDescending,
            string additionalParams = null)
        {
            var paginationParams = client.GetPaginationQueryString(paginationKey, pageNumber, getTotalCount, isDescending);
            var queryString = CombineQueryStrings(paginationParams, additionalParams);

            var response = await client.HttpClient.GetAsync($"{url}{queryString}");
            if (!response.IsSuccessStatusCode)
                return await response.GetPaginatedErrorResult<TEntity>();

            var data = JsonConvert.DeserializeAnonymousType(
                await response.Content.ReadAsStringAsync(),
                anonymousTypeDefinition,
                Global.JsonSerializerSettings);
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
