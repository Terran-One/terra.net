using System.Collections.Generic;
using System.Threading.Tasks;
using Terra.Sdk.Lcd.Extensions;
using Terra.Sdk.Lcd.Models;

namespace Terra.Sdk.Lcd.Api
{
    public class Bank
    {
        private readonly LcdClient _lcdClient;

        internal Bank(LcdClient lcdClient)
        {
            _lcdClient = lcdClient;
        }

        public Task<PaginatedResult<Coin>> GetBalance(string address, string paginationKey = null, int? pageNumber = null, bool? getTotalCount = null, bool? isDescending = null)
        {
            return _lcdClient.GetPaginatedResult(
                $"/cosmos/bank/v1beta1/balances/{address}",
                new
                {
                    data = new List<Coin>(),
                    pagination = new { next_key = "", total = 0 }
                },
                data => new PaginatedResult<Coin>
                {
                    Value = data.data,
                    TotalCount = data.pagination?.total,
                    NextPageKey = data.pagination?.next_key,
                    NextPageNumber = pageNumber + 1
                },
                paginationKey, pageNumber, getTotalCount, isDescending);
        }

        public Task<PaginatedResult<Coin>> GetSupply(string paginationKey = null, int? pageNumber = null, bool? getTotalCount = null, bool? isDescending = null)
        {
            return _lcdClient.GetPaginatedResult(
                "/cosmos/bank/v1beta1/supply",
                new
                {
                    supply = new List<Coin>(),
                    pagination = new { next_key = "", total = 0 }
                },
                data => new PaginatedResult<Coin>
                {
                    Value = data.supply,
                    TotalCount = data.pagination?.total,
                    NextPageKey = data.pagination?.next_key,
                    NextPageNumber = pageNumber + 1
                },
                paginationKey, pageNumber, getTotalCount, isDescending);
        }
    }
}
