using Terra.Sdk.Lcd.Models.Entities.Tx.Msg.IbcChannelMsg.Primitives;
using ProtoBuf;

namespace Terra.Sdk.Lcd.Models.Entities.Tx.Msg.IbcChannelMsg
{
    [ProtoContract]
    public class MsgChannelCloseConfirm : Msg
    {
        protected override System.Type Type => typeof(MsgChannelCloseConfirm);

        [ProtoMember(1, Name = "port_id")] public string PortId { get; set; }
        [ProtoMember(2, Name = "channel_id")] public string ChannelId { get; set; }
        [ProtoMember(3, Name = "proof_init")] public string ProofInit { get; set; }
        [ProtoMember(4, Name = "proof_height")] public Height ProofHeight { get; set; }
        [ProtoMember(5, Name = "signer")] public string Signer { get; set; }
    }
}
