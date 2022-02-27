using System;
using System.Threading.Tasks;
using JsonSubTypes;
using Newtonsoft.Json;

namespace Terra.Sdk.Lcd.Models.Entities
{
    [JsonConverter(typeof(JsonSubtypes), "@type")]
    [JsonSubtypes.KnownSubType(typeof(SendAuthorization), "/cosmos.bank.v1beta1.SendAuthorization")]
    [JsonSubtypes.KnownSubType(typeof(GenericAuthorization), "/cosmos.authz.v1beta1.GenericAuthorization")]
    [JsonSubtypes.KnownSubType(typeof(StakeAuthorization), "/cosmos.staking.v1beta1.StakeAuthorization")]
    public class Authorization
    {
        private readonly LcdClient _client;

        public Authorization()
        {
        }

        public Authorization(LcdClient client)
        {
            _client = client;
        }

        public async Task<PaginatedResult<Authorization>> Get(
            string granter, string grantee, string msgTypeUrl = null,
            string paginationKey = null, int? pageNumber = null, bool? getTotalCount = null, bool? isDescending = null)
        {
            throw new NotImplementedException();
        }
    }

    public class SendAuthorization : Authorization
    {
        
    }

    public class GenericAuthorization : Authorization
    {
        
    }

    public class StakeAuthorization : Authorization
    {
        
    }
}