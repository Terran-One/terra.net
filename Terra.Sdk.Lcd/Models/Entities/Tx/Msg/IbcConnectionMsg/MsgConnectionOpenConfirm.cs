using Terra.Sdk.Lcd.Models.Entities.Tx.Msg.IbcChannelMsg.Primitives;
using ProtoBuf;

namespace Terra.Sdk.Lcd.Models.Entities.Tx.Msg.IbcConnectionMsg
{
    [ProtoContract]
    public class MsgConnectionOpenConfirm : Msg
    {
        [ProtoMember(1, Name = "connection_id")] public string ConnectionId { get; set; }
        [ProtoMember(2, Name = "proof_ack")] public string ProofAck { get; set; }
        [ProtoMember(3, Name = "proof_height")] public Height ProofHeight { get; set; }
        [ProtoMember(4, Name = "signer")] public string Signer { get; set; }
    }
}
