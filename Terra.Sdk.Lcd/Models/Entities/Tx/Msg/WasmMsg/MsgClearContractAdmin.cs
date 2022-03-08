using ProtoBuf;

namespace Terra.Sdk.Lcd.Models.Entities.Tx.Msg.WasmMsg
{
    [ProtoContract]
    public class MsgClearContractAdmin : Msg
    {
        [ProtoMember(1)] public string Admin { get; set; }
        [ProtoMember(2)] public string Contract { get; set; }
    }
}