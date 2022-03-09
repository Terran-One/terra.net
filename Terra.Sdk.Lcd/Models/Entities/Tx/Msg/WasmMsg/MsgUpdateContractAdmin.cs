using ProtoBuf;

namespace Terra.Sdk.Lcd.Models.Entities.Tx.Msg.WasmMsg
{
    [ProtoContract]
    public class MsgUpdateContractAdmin : Msg
    {
        [ProtoMember(1, Name = "admin")] public string Admin { get; set; }
        [ProtoMember(2, Name = "new_admin")] public string NewAdmin { get; set; }
        [ProtoMember(3, Name = "contract")] public string Contract { get; set; }
    }
}
