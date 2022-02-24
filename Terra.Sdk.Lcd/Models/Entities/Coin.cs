using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Terra.Sdk.Lcd.Models.Entities
{
    public class Coin
    {
        [JsonProperty("denom")]
        public string Denom { get; set; }

        [JsonProperty("amount")]
        public decimal Amount { get; set; }

        internal static async Task<Result<Coin[]>> Balance(string address, HttpClient httpClient, QueryParams queryParams = null)
        {
            var response = await httpClient.GetAsync($"/cosmos/bank/v1beta1/balances/{address}{queryParams}");
            if (!response.IsSuccessStatusCode)
                return new Result<Coin[]>($"Fetch failed: {response.ReasonPhrase}");

            var json = JsonConvert.DeserializeAnonymousType(await response.Content.ReadAsStringAsync(), new
            {
                data = Array.Empty<Coin>(),
                pagination = new { next_key = "", total = 0 }
            });

            return new Result<Coin[]>(
                json.data, json.pagination.next_key, json.pagination.total, queryParams,
                qp => Balance(address, httpClient, qp));
        }

        internal static async Task<Result<Coin[]>> Total(HttpClient httpClient, QueryParams queryParams = null)
        {
            var response = await httpClient.GetAsync($"/cosmos/bank/v1beta1/supply{queryParams}");
            if (!response.IsSuccessStatusCode)
                return new Result<Coin[]>($"Fetch failed: {response.ReasonPhrase}");

            var json = JsonConvert.DeserializeAnonymousType(await response.Content.ReadAsStringAsync(), new
            {
                supply = Array.Empty<Coin>(),
                pagination = new { next_key = "", total = 0 }
            });

            return new Result<Coin[]>(
                json.supply, json.pagination.next_key, json.pagination.total, queryParams,
                qp => Total(httpClient, qp));
        }
    }
}
