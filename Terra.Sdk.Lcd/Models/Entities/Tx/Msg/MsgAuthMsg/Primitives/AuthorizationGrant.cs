using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading.Tasks;
using System.Web;
using Terra.Sdk.Lcd.Extensions;

namespace Terra.Sdk.Lcd.Models.Entities.Tx.Msg.MsgAuthMsg.Primitives
{
    public class AuthorizationGrant
    {
        private readonly LcdClient _client;

        /// <remarks>
        /// For serialization.
        /// </remarks>
        public AuthorizationGrant()
        {
        }

        public AuthorizationGrant(LcdClient client)
        {
            _client = client;
        }

        public Authorization.Authorization Authorization { get; set; }
        public DateTime Expiration { get; set; }

        internal Task<PaginatedResult<AuthorizationGrant>> Get(
            string granter, string grantee, string msgTypeUrl = null,
            string paginationKey = null, int? pageNumber = null, bool? getTotalCount = null, bool? isDescending = null)
        {
            return _client.GetPaginatedResult(
                "/cosmos/authz/v1beta1/grants",
                new
                {
                    Grants = new List<AuthorizationGrant>(),
                    Pagination = new { NextKey = "", Total = 0 }
                },
                data => new PaginatedResult<AuthorizationGrant> { Value = data.Grants },
                paginationKey, pageNumber, getTotalCount, isDescending,
                new NameValueCollection
                {
                    { "granter", granter },
                    { "grantee", grantee },
                    { "msg_type_url", msgTypeUrl}
                });
        }
    }
}
