using Terra.Sdk.Lcd.Models.Entities.Tx.Msg.IbcChannelMsg.Primitives;
using ProtoBuf;

namespace Terra.Sdk.Lcd.Models.Entities.Tx.Msg.IbcChannelMsg
{
    [ProtoContract]
    public class MsgAcknowledgement : Msg
    {
        [ProtoMember(1, Name = "packet")] public Packet Packet { get; set; }
        [ProtoMember(2, Name = "acknowledgement")] public string Acknowledgement { get; set; }
        [ProtoMember(3, Name = "proof_acked")] public string ProofAcked { get; set; }
        [ProtoMember(4, Name = "proof_height")] public Height ProofHeight { get; set; }
        [ProtoMember(5, Name = "signer")] public string Signer { get; set; }
    }
}
