using ProtoBuf; namespace Terra.Sdk.Lcd.Models.Entities.Tx.Msg.IbcChannelMsg
{
    [ProtoContract]public class MsgChannelCloseInit : Msg
    {
        [ProtoMember(1)]public string PortId { get; set; }
        [ProtoMember(2)]public string ChannelId { get; set; }
        [ProtoMember(3)]public string Signer { get; set; }
    }
}
