using Terra.Sdk.Lcd.Models.Entities.Tx.Msg.IbcChannelMsg.Primitives;
using ProtoBuf;

namespace Terra.Sdk.Lcd.Models.Entities.Tx.Msg.IbcChannelMsg
{
    [ProtoContract]
    public class MsgTimeout : Msg
    {
        protected override System.Type Type => typeof(MsgTimeout);

        [ProtoMember(1, Name = "packet")] public Packet Packet { get; set; }
        [ProtoMember(2, Name = "proof_unreceived")] public string ProofUnreceived { get; set; }
        [ProtoMember(3, Name = "proof_height")] public Height ProofHeight { get; set; }
        [ProtoMember(4, Name = "next_sequence_recv")] public long NextSequenceRecv { get; set; }
        [ProtoMember(5, Name = "signer")] public string Signer { get; set; }
    }
}
