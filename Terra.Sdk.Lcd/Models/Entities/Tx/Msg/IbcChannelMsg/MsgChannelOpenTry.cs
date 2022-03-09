using Terra.Sdk.Lcd.Models.Entities.Tx.Msg.IbcChannelMsg.Primitives;
using ProtoBuf;

namespace Terra.Sdk.Lcd.Models.Entities.Tx.Msg.IbcChannelMsg
{
    [ProtoContract]
    public class MsgChannelOpenTry : Msg
    {
        protected override System.Type Type => typeof(MsgChannelOpenTry);

        [ProtoMember(1, Name = "port_id")] public string PortId { get; set; }
        [ProtoMember(2, Name = "previous_channel_id")] public string PreviousChannelId { get; set; }
        [ProtoMember(3, Name = "channel")] public Channel Channel { get; set; }
        [ProtoMember(4, Name = "counterparty_version")] public string CounterpartyVersion { get; set; }
        [ProtoMember(5, Name = "proof_init")] public string ProofInit { get; set; }
        [ProtoMember(6, Name = "proof_height")] public Height ProofHeight { get; set; }
        [ProtoMember(7, Name = "signer")] public string Signer { get; set; }
    }
}
