using ProtoBuf;

namespace Terra.Sdk.Lcd.Models.Entities.Tx.Msg.WasmMsg
{
    [ProtoContract]
    public class MsgStoreCode : Msg
    {
        protected override System.Type Type => typeof(MsgStoreCode);

        [ProtoMember(1, Name = "sender")] public string Sender { get; set; }
        [ProtoMember(2, Name = "wasm_byte_code")] public string WasmByteCode { get; set; }
    }
}
