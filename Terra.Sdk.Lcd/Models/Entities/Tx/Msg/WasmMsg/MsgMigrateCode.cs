using ProtoBuf;

namespace Terra.Sdk.Lcd.Models.Entities.Tx.Msg.WasmMsg
{
    [ProtoContract]
    public class MsgMigrateCode : Msg
    {
        [ProtoMember(1, Name = "sender")] public string Sender { get; set; }
        [ProtoMember(2, Name = "code_id")] public long CodeId { get; set; }
        [ProtoMember(3, Name = "wasm_byte_code")] public string WasmByteCode { get; set; }
    }
}
