namespace Terra.Sdk.Lcd.Models.Entities.Tx.Msg.MsgAuthMsg
{
    public class MsgRevokeAuthorization : Msg
    {
        public string Granter { get; set; }
        public string Grantee { get; set; }
        public string MsgTypeUrl { get; set; }
    }
}
