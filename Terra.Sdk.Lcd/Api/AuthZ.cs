using System.Threading.Tasks;
using Terra.Sdk.Lcd.Models;
using Terra.Sdk.Lcd.Models.Entities.Authorization;

namespace Terra.Sdk.Lcd.Api
{
    public class AuthZ
    {
        private readonly LcdClient _client;

        internal AuthZ(LcdClient client)
        {
            _client = client;
        }

        public Task<PaginatedResult<Authorization>> GetGrants(
            string granter, string grantee, string msgTypeUrl = null,
            string paginationKey = null, int? pageNumber = null, bool? getTotalCount = null, bool? isDescending = null)
        {
            return new Authorization(_client).Get(
                granter, grantee, msgTypeUrl,
                paginationKey, pageNumber, getTotalCount, isDescending);
        }
    }
}
