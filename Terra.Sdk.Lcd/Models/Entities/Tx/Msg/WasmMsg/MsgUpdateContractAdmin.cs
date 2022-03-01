namespace Terra.Sdk.Lcd.Models.Entities.Tx.Msg.WasmMsg
{
    public class MsgUpdateContractAdmin : Msg
    {
        public string Admin { get; set; }
        public string NewAdmin { get; set; }
        public string Contract { get; set; }
    }
}
