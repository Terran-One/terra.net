namespace Terra.Sdk.Lcd.Models.Entities.Tx.Msg.FeeGrantMsg
{
    public class MsgGrantAllowance : Msg
    {
        public string Granter { get; set; }
        public string Grantee { get; set; }
        public Models.Entities.Allowance.Allowance Allowance { get; set; }
    }
}
