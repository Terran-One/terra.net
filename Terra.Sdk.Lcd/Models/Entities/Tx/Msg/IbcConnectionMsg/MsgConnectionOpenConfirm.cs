using Terra.Sdk.Lcd.Models.Entities.Tx.Msg.IbcChannelMsg.Primitives;

using ProtoBuf; namespace Terra.Sdk.Lcd.Models.Entities.Tx.Msg.IbcConnectionMsg
{
    [ProtoContract]public class MsgConnectionOpenConfirm : Msg
    {
        [ProtoMember(1)]public string ConnectionId { get; set; }
        [ProtoMember(2)]public string ProofAck { get; set; }
        [ProtoMember(3)]public Height ProofHeight { get; set; }
        [ProtoMember(4)]public string Signer { get; set; }
    }
}
