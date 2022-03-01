using System;

namespace Terra.Sdk.Lcd.Models.Entities.Tx.Msg.MsgAuthMsg.Primitives
{
    public class AuthorizationGrant
    {
        public Authorization.Authorization Authorization { get; set; }
        public DateTime Expiration { get; set; }
    }
}
