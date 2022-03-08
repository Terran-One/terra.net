using ProtoBuf; namespace Terra.Sdk.Lcd.Models.Entities.Tx.Msg.WasmMsg
{
    [ProtoContract]public class MsgStoreCode : Msg
    {
        [ProtoMember(1)]public string Sender { get; set; }
        [ProtoMember(2)]public string WasmByteCode { get; set; }
    }
}
