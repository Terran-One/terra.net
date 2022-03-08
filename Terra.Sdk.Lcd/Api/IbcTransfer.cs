using System.Threading.Tasks;
using Terra.Sdk.Lcd.Models;
using Terra.Sdk.Lcd.Models.Entities.Ibc;

namespace Terra.Sdk.Lcd.Api
{
    public class IbcTransfer
    {
        private readonly LcdClient _client;

        internal IbcTransfer(LcdClient client)
        {
            _client = client;
        }

        public Task<Result<DenomTrace>> GetDenomTrace(string hash) => new DenomTrace(_client).Get(hash);

        public Task<PaginatedResult<DenomTrace>> GetDenomTraces(string paginationKey = null, int? pageNumber = null, bool? getTotalCount = null, bool? isDescending = null) =>
            new DenomTrace(_client).GetAll(paginationKey, pageNumber, getTotalCount, isDescending);

        public Task<Result<IbcTransferParams>> GetParameters() => new IbcTransferParams(_client).Get();
    }
}