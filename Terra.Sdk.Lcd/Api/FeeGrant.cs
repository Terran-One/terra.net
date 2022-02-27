using System.Threading.Tasks;
using Terra.Sdk.Lcd.Models;
using Terra.Sdk.Lcd.Models.Entities;

namespace Terra.Sdk.Lcd.Api
{
    public class FeeGrant
    {
        private readonly LcdClient _client;

        public FeeGrant(LcdClient client)
        {
            _client = client;
        }

        public Task<Result<Allowance>> GetAllowance(string granter, string grantee) => new Allowance(_client).Get(granter, grantee);

        public Task<PaginatedResult<Allowance>> GetAllowances(string grantee, string paginationKey = null, int? pageNumber = null, bool? getTotalCount = null, bool? isDescending = null) =>
            new Allowance(_client).GetAll(grantee, paginationKey, pageNumber, getTotalCount, isDescending);
    }
}