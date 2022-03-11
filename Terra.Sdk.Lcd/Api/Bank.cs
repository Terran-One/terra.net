using System.Collections.Generic;
using System.Threading.Tasks;
using Terra.Sdk.Lcd.Extensions;
using Terra.Sdk.Lcd.Models;
using Terra.Sdk.Lcd.Models.Entities;

namespace Terra.Sdk.Lcd.Api
{
    public class Bank
    {
        private readonly LcdClient _client;

        internal Bank(LcdClient lcdClient)
        {
            _client = lcdClient;
        }

        public Task<PaginatedResult<Coin>> GetBalance(string address, string paginationKey = null, int? pageNumber = null, bool? getTotalCount = null, bool? isDescending = null)
        {
            return _client.GetPaginatedResult(
                $"/cosmos/bank/v1beta1/balances/{address}",
                new
                {
                    Balances = new List<Coin>(),
                    Pagination = new Pagination()
                },
                data => data.Pagination.BuildResult(data.Balances, pageNumber),
                paginationKey, pageNumber, getTotalCount, isDescending);
        }

        public Task<PaginatedResult<Coin>> GetSupply(string paginationKey = null, int? pageNumber = null, bool? getTotalCount = null, bool? isDescending = null)
        {
            return _client.GetPaginatedResult(
                "/cosmos/bank/v1beta1/supply",
                new
                {
                    Supply = new List<Coin>(),
                    Pagination = new Pagination()
                },
                data => data.Pagination.BuildResult(data.Supply, pageNumber),
                paginationKey, pageNumber, getTotalCount, isDescending);
        }
    }
}
