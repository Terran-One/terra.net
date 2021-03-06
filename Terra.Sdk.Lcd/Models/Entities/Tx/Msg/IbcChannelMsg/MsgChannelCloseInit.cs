using ProtoBuf;

namespace Terra.Sdk.Lcd.Models.Entities.Tx.Msg.IbcChannelMsg
{
    [ProtoContract]
    public class MsgChannelCloseInit : Msg
    {
        protected override System.Type Type => typeof(MsgChannelCloseInit);

        [ProtoMember(1, Name = "port_id")] public string PortId { get; set; }
        [ProtoMember(2, Name = "channel_id")] public string ChannelId { get; set; }
        [ProtoMember(3, Name = "signer")] public string Signer { get; set; }
    }
}
