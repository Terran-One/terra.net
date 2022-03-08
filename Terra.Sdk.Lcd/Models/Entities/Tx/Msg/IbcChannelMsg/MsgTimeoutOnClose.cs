using Terra.Sdk.Lcd.Models.Entities.Tx.Msg.IbcChannelMsg.Primitives;
using ProtoBuf;

namespace Terra.Sdk.Lcd.Models.Entities.Tx.Msg.IbcChannelMsg
{
    [ProtoContract]
    public class MsgTimeoutOnClose : Msg
    {
        [ProtoMember(1)] public Packet Packet { get; set; }
        [ProtoMember(2)] public string ProofUnreceived { get; set; }
        [ProtoMember(3)] public string ProofClose { get; set; }
        [ProtoMember(4)] public Height ProofHeight { get; set; }
        [ProtoMember(5)] public long NextSequenceRecv { get; set; }
        [ProtoMember(6)] public string Signer { get; set; }
    }
}