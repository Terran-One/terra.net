namespace Terra.Sdk.Lcd.Models.Entities.Tx.Msg.WasmMsg
{
    public class MsgClearContractAdmin : Msg
    {
        public string Admin { get; set; }
        public string Contract { get; set; }
    }
}
