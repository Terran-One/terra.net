namespace Terra.Sdk.Lcd.Models.Entities.Tx.Msg.FeeGrantMsg
{
    public class MsgRevokeAllowance : Msg
    {
        public string Granter { get; set; }
        public string Grantee { get; set; }
    }
}
