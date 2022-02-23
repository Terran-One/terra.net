using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Terra.Sdk.Lcd.Dto;

namespace Terra.Sdk.Lcd.Models
{
    public class Coin
    {
        [JsonProperty("denom")]
        public string Denom { get; set; }

        [JsonProperty("amount")]
        public decimal Amount { get; set; }

        public static async Task<Result<Coin[]>> Balance(string address, LcdClientConfig config)
        {
            var response = await config.HttpClient.GetAsync($"/cosmos/bank/v1beta1/balances/{address}{config.GetQueryParams()}");
            if (!response.IsSuccessStatusCode)
                return new Result<Coin[]>($"Fetch failed: {response.ReasonPhrase}"); // ToDo: something better than this

            var json = JsonConvert.DeserializeAnonymousType(await response.Content.ReadAsStringAsync(), new
            {
                data = Array.Empty<Coin>(),
                pagination = new Pagination()
            });

            return new Result<Coin[]>(json.data, json.pagination);
        }

        public static async Task<Result<Coin[]>> Total(LcdClientConfig config)
        {
            var response = await config.HttpClient.GetAsync($"/cosmos/bank/v1beta1/supply{config.GetQueryParams()}");
            if (!response.IsSuccessStatusCode)
                return new Result<Coin[]>($"Fetch failed: {response.ReasonPhrase}"); // ToDo: something better than this

            var json = JsonConvert.DeserializeAnonymousType(await response.Content.ReadAsStringAsync(), new
            {
                supply = Array.Empty<Coin>(),
                pagination = new Pagination()
            });

            return new Result<Coin[]>(json.supply, json.pagination);
        }
    }
}
