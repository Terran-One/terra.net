using System;
using System.Collections.Generic;
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
            var query = HttpUtility.ParseQueryString(string.Empty);
            query["granter"] = granter;
            query["grantee"] = grantee;
            if (!string.IsNullOrWhiteSpace(msgTypeUrl))
                query["msg_type_url"] = msgTypeUrl;

            return _client.GetPaginatedResult(
                "/cosmos/authz/v1beta1/grants",
                new
                {
                    Grants = new List<AuthorizationGrant>(),
                    Pagination = new { NextKey = "", Total = 0 }
                },
                data => new PaginatedResult<AuthorizationGrant> { Value = data.Grants },
                paginationKey, pageNumber, getTotalCount, isDescending,
                query.ToString());
        }
    }
}
