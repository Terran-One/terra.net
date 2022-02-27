using System;
using System.Threading.Tasks;
using Terra.Sdk.Lcd.Models;
using Terra.Sdk.Lcd.Models.Entities;

namespace Terra.Sdk.Lcd.Api
{
    public class IbcTransfer
    {
        private readonly LcdClient _client;

        public IbcTransfer(LcdClient client)
        {
            _client = client;
        }

        public Task<Result<DenomTrace>> GetDenomTrace(string hash)
        {
            throw new NotImplementedException();
        }

        public Task<PaginatedResult<DenomTrace>> GetDenomTraces(
            string paginationKey = null, int? pageNumber = null, bool? getTotalCount = null, bool? isDescending = null)
        {
            throw new NotImplementedException();
        }

        public Task<Result<IbcTransferParams>> GetParameters()
        {
            throw new NotImplementedException();
        }
    }
}