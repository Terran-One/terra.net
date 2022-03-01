namespace Terra.Sdk.Lcd.Models.Entities.Tx.Msg.OracleMsg
{
    public class MsgDelegateFeedConsent : Msg
    {
        public string Operator { get; set; }
        public string Delegate { get; set; }
    }
}
