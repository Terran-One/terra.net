using System.Net.Http;
using System.Threading.Tasks;
using Terra.Sdk.Lcd.Models;
using Terra.Sdk.Lcd.Models.Entities;

namespace Terra.Sdk.Lcd.Api
{
    public class BankApi
    {
        private readonly HttpClient _httpClient;

        public BankApi(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Model<Coin[]>> Balance(string address, QueryParams queryParams = null)
        {
            return await Coin.Balance(address, _httpClient, queryParams);
        }

        public async Task<Model<Coin[]>> Total(QueryParams queryParams = null)
        {
            return await Coin.Total(_httpClient, queryParams);
        }
    }
}
