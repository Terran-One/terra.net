using ProtoBuf; namespace Terra.Sdk.Lcd.Models.Entities.Tx.Msg.WasmMsg
{
    [ProtoContract]public class MsgUpdateContractAdmin : Msg
    {
        [ProtoMember(1)]public string Admin { get; set; }
        [ProtoMember(2)]public string NewAdmin { get; set; }
        [ProtoMember(3)]public string Contract { get; set; }
    }
}
