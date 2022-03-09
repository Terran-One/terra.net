using ProtoBuf;

namespace Terra.Sdk.Lcd.Models.Entities.Tx.Msg.WasmMsg
{
    [ProtoContract]
    public class MsgClearContractAdmin : Msg
    {
        protected override System.Type Type => typeof(MsgClearContractAdmin);

        [ProtoMember(1, Name = "admin")] public string Admin { get; set; }
        [ProtoMember(2, Name = "contract")] public string Contract { get; set; }
    }
}
