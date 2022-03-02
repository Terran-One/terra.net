using System;
using System.Threading.Tasks;
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
            string paginationKey, int? pageNumber, bool? getTotalCount, bool? isDescending)
        {
            var response = await lcdClient.HttpClient.GetAsync($"{url}{lcdClient.GetPaginationQueryString(paginationKey, pageNumber, getTotalCount, isDescending)}");
            if (!response.IsSuccessStatusCode)
                return new PaginatedResult<TEntity> { Error = $"Fetch failed: {response.ReasonPhrase}" };

            var data = JsonConvert.DeserializeAnonymousType(
                await response.Content.ReadAsStringAsync(),
                anonymousTypeDefinition,
                lcdClient.JsonSerializerSettings);
            return resultBuilder(data);
        }
    }
}
