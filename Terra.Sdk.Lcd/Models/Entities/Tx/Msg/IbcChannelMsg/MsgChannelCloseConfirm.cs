using Terra.Sdk.Lcd.Models.Entities.Tx.Msg.IbcChannelMsg.Primitives;
using ProtoBuf;

namespace Terra.Sdk.Lcd.Models.Entities.Tx.Msg.IbcChannelMsg
{
    [ProtoContract]
    public class MsgChannelCloseConfirm : Msg
    {
        [ProtoMember(1)] public string PortId { get; set; }
        [ProtoMember(2)] public string ChannelId { get; set; }
        [ProtoMember(3)] public string ProofInit { get; set; }
        [ProtoMember(4)] public Height ProofHeight { get; set; }
        [ProtoMember(5)] public string Signer { get; set; }
    }
}