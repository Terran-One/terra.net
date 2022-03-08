using Terra.Sdk.Lcd.Models.Entities.Tx.Msg.IbcChannelMsg.Primitives;
using ProtoBuf;

namespace Terra.Sdk.Lcd.Models.Entities.Tx.Msg.IbcChannelMsg
{
    [ProtoContract]
    public class MsgRecvPacket : Msg
    {
        [ProtoMember(1)] public Packet Packet { get; set; }
        [ProtoMember(2)] public string ProofCommitment { get; set; }
        [ProtoMember(3)] public Height ProofHeight { get; set; }
        [ProtoMember(4)] public string Signer { get; set; }
    }
}