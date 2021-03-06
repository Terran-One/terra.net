using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading.Tasks;
using Terra.Sdk.Lcd.Extensions;
using ProtoBuf;

namespace Terra.Sdk.Lcd.Models.Entities.Tx.Msg.MsgAuthMsg.Primitives
{
    [ProtoContract]
    public class AuthorizationGrant
    {
        private readonly LcdClient _client;

        /// <remarks>
        /// For serialization.
        /// </remarks>
        public AuthorizationGrant()
        {
        }

        internal AuthorizationGrant(LcdClient client)
        {
            _client = client;
        }

        [ProtoMember(1, Name = "authorization")] public Authorization.Authorization Authorization { get; set; }
        [ProtoMember(2, Name = "expiration")] public DateTime Expiration { get; set; }

        internal Task<PaginatedResult<AuthorizationGrant>> Get(
            string granter, string grantee, string msgTypeUrl = null,
            string paginationKey = null, int? pageNumber = null, bool? getTotalCount = null, bool? isDescending = null)
        {
            return _client.GetPaginatedResult(
                "/cosmos/authz/v1beta1/grants",
                new
                {
                    Grants = new List<AuthorizationGrant>(),
                    Pagination = new Pagination()
                },
                data => data.Pagination.BuildResult(data.Grants, pageNumber),
                paginationKey, pageNumber, getTotalCount, isDescending,
                new NameValueCollection
                {
                    {"granter", granter},
                    {"grantee", grantee},
                    {"msg_type_url", msgTypeUrl}
                }.ToQueryString());
        }
    }
}
