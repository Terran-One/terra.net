using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using Terra.Sdk.Lcd.Dto;
using Terra.Sdk.Lcd.Models;

namespace Terra.Sdk.Lcd.Api
{
    public class BankApi
    {
        private readonly HttpClient _httpClient;

        public BankApi(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Result<Dto.Coin[]>> Balance(string address, PaginationOptions pagination = null, IDictionary<string, object> apiParams = null)
        {
            var response = await _httpClient.GetAsync($"/cosmos/bank/v1beta1/balances/{address}{GetParams(pagination, apiParams)}");
            if (!response.IsSuccessStatusCode)
                return new Result<Dto.Coin[]>($"Fetch failed: {response.ReasonPhrase}"); // ToDo: something better than this

            var json = JsonConvert.DeserializeAnonymousType(await response.Content.ReadAsStringAsync(), new
            {
                data = Array.Empty<Dto.Coin>(),
                pagination = new Pagination()
            });

            return new Result<Dto.Coin[]>(json.data, json.pagination);
        }

        public async Task<Result<Dto.Coin[]>> Total(PaginationOptions pagination = null, IDictionary<string, object> apiParams = null)
        {
            var response = await _httpClient.GetAsync($"/cosmos/bank/v1beta1/supply{GetParams(pagination, apiParams)}");
            if (!response.IsSuccessStatusCode)
                return new Result<Dto.Coin[]>($"Fetch failed: {response.ReasonPhrase}");

            var json = JsonConvert.DeserializeAnonymousType(await response.Content.ReadAsStringAsync(), new
            {
                supply = Array.Empty<Dto.Coin>(),
                pagination = new Pagination()
            });

            return new Result<Dto.Coin[]>(json.supply, json.pagination);
        }

        private static string GetParams(PaginationOptions pagination = null,
            IDictionary<string, object> apiParams = null)
        {
            var paginationParams = pagination?.ToString();
            if (string.IsNullOrWhiteSpace(paginationParams))
                paginationParams = "?";
            else
                paginationParams += "&";

            var api = HttpUtility.ParseQueryString(string.Empty);
            foreach (var kvp in apiParams)
                api[kvp.Key] = kvp.Value.ToString();

            paginationParams += api.ToString();
            return paginationParams.TrimEnd('&');
        }
    }
}
