using Terra.Sdk.Lcd.Models.Entities.Tx.Msg.MsgAuthMsg.Primitives;

namespace Terra.Sdk.Lcd.Models.Entities.Tx.Msg.MsgAuthMsg
{
    public class MsgGrantAuthorization : Msg
    {
        public string Granter { get; set; }
        public string Grantee { get; set; }
        public AuthorizationGrant Grant { get; set; }
    }
}
