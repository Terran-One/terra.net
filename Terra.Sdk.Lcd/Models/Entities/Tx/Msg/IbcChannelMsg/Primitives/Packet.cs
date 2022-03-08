using System;

using ProtoBuf; namespace Terra.Sdk.Lcd.Models.Entities.Tx.Msg.IbcChannelMsg.Primitives
{
    [ProtoContract]
    public class Packet
    {
        [ProtoMember(1)]public long Sequence { get; set; }
        [ProtoMember(2)]public string SourcePort { get; set; }
        [ProtoMember(3)]public string SourceChannel { get; set; }
        [ProtoMember(4)]public string DestinationPort { get; set; }
        [ProtoMember(5)]public string DestinationChannel { get; set; }
        [ProtoMember(6)]public string Data { get; set; }
        [ProtoMember(7)]public Height TimeoutHeight { get; set; }
        [ProtoMember(8)]public DateTime TimeoutTimestamp { get; set; }
    }
}
