using System;

namespace Terra.Sdk.Lcd.Models.Entities.Tx.Msg.IbcChannelMsg.Primitives
{
    public class Packet
    {
        public long Sequence { get; set; }
        public string SourcePort { get; set; }
        public string SourceChannel { get; set; }
        public string DestinationPort { get; set; }
        public string DestinationChannel { get; set; }
        public string Data { get; set; }
        public Height TimeoutHeight { get; set; }
        public DateTime TimeoutTimestamp { get; set; }
    }
}
