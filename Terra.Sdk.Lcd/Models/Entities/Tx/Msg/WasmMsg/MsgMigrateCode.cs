namespace Terra.Sdk.Lcd.Models.Entities.Tx.Msg.WasmMsg
{
    public class MsgMigrateCode : Msg
    {
        public string Sender { get; set; }
        public long CodeId { get; set; }
        public string WasmByteCode { get; set; }
    }
}
