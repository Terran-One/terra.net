using System.Threading.Tasks;
using Terra.Sdk.Lcd.Models;
using Terra.Sdk.Lcd.Models.Entities;

namespace Terra.Sdk.Lcd.Api
{
    public class Slashing
    {
        private readonly LcdClient _client;

        internal Slashing(LcdClient client)
        {
            _client = client;
        }

        public Task<Result<SigningInfo>> GetSigningInfo(string valConsAddress) => new SigningInfo(_client).Get(valConsAddress);

        public Task<PaginatedResult<SigningInfo>> GetSigningInfos(string paginationKey = null, int? pageNumber = null, bool? getTotalCount = null, bool? isDescending = null) =>
            new SigningInfo(_client).GetAll(paginationKey, pageNumber, getTotalCount, isDescending);

        public Task<Result<SlashingParams>> GetParameters() => new SlashingParams(_client).Get();
    }
}