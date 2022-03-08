using ProtoBuf; namespace Terra.Sdk.Lcd.Models.Entities.Tx.Msg.WasmMsg
{
    [ProtoContract]public class MsgMigrateCode : Msg
    {
        [ProtoMember(1)]public string Sender { get; set; }
        [ProtoMember(2)]public long CodeId { get; set; }
        [ProtoMember(3)]public string WasmByteCode { get; set; }
    }
}
