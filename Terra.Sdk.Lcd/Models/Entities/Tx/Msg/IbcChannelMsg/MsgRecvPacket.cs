using Terra.Sdk.Lcd.Models.Entities.Tx.Msg.IbcChannelMsg.Primitives;
using ProtoBuf;

namespace Terra.Sdk.Lcd.Models.Entities.Tx.Msg.IbcChannelMsg
{
    [ProtoContract]
    public class MsgRecvPacket : Msg
    {
        protected override System.Type Type => typeof(MsgRecvPacket);

        [ProtoMember(1, Name = "packet")] public Packet Packet { get; set; }
        [ProtoMember(2, Name = "proof_commitment")] public string ProofCommitment { get; set; }
        [ProtoMember(3, Name = "proof_height")] public Height ProofHeight { get; set; }
        [ProtoMember(4, Name = "signer")] public string Signer { get; set; }
    }
}
