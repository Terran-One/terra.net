using ProtoBuf; namespace Terra.Sdk.Lcd.Models.Entities.Tx.Msg.IbcChannelMsg.Primitives
{
    [ProtoContract]
    public class Counterparty
    {
        [ProtoMember(1)]public string PortId { get; set; }
        [ProtoMember(2)]public string ChannelId { get; set; }
    }
}
