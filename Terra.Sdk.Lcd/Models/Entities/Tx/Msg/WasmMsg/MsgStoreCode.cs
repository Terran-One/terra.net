namespace Terra.Sdk.Lcd.Models.Entities.Tx.Msg.WasmMsg
{
    public class MsgStoreCode : Msg
    {
        public string Sender { get; set; }
        public string WasmByteCode { get; set; }
    }
}
