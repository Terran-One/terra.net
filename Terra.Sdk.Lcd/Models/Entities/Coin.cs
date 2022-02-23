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

        public static async Task<Model<Coin[]>> Balance(string address, HttpClient httpClient, QueryParams queryParams = null)
        {
            var response = await httpClient.GetAsync($"/cosmos/bank/v1beta1/balances/{address}{queryParams}");
            if (!response.IsSuccessStatusCode)
                return new Model<Coin[]>($"Fetch failed: {response.ReasonPhrase}");

            var json = JsonConvert.DeserializeAnonymousType(await response.Content.ReadAsStringAsync(), new
            {
                data = Array.Empty<Coin>(),
                pagination = new Pagination()
            });

            return new Model<Coin[]>(json.data, json.pagination);
        }

        public static async Task<Model<Coin[]>> Total(HttpClient httpClient, QueryParams queryParams = null)
        {
            var response = await httpClient.GetAsync($"/cosmos/bank/v1beta1/supply{queryParams}");
            if (!response.IsSuccessStatusCode)
                return new Model<Coin[]>($"Fetch failed: {response.ReasonPhrase}");

            var json = JsonConvert.DeserializeAnonymousType(await response.Content.ReadAsStringAsync(), new
            {
                supply = Array.Empty<Coin>(),
                pagination = new Pagination()
            });

            return new Model<Coin[]>(json.supply, json.pagination);
        }
    }
}
