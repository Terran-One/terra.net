using System;
using ProtoBuf;

namespace Terra.Sdk.Lcd.Models.Entities.Tx.Msg.IbcChannelMsg.Primitives
{
    [ProtoContract]
    public class Packet
    {
        [ProtoMember(1, Name = "sequence")] public long Sequence { get; set; }
        [ProtoMember(2, Name = "source_port")] public string SourcePort { get; set; }
        [ProtoMember(3, Name = "source_channel")] public string SourceChannel { get; set; }
        [ProtoMember(4, Name = "destination_port")] public string DestinationPort { get; set; }
        [ProtoMember(5, Name = "destination_channel")] public string DestinationChannel { get; set; }
        [ProtoMember(6, Name = "data")] public string Data { get; set; }
        [ProtoMember(7, Name = "timeout_height")] public Height TimeoutHeight { get; set; }
        [ProtoMember(8, Name = "timeout_timestamp")] public DateTime TimeoutTimestamp { get; set; }
    }
}
