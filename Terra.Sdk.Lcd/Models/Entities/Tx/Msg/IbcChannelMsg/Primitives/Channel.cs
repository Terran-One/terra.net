using System.Collections.Generic;

namespace Terra.Sdk.Lcd.Models.Entities.Tx.Msg.IbcChannelMsg.Primitives
{
    public class Channel
    {
        public State State { get; set; }
        public Order Ordering { get; set; }
        public Counterparty Counterparty { get; set; }
        public List<string> ConnectionHops { get; set; }
        public string Version { get; set; }
    }
}
