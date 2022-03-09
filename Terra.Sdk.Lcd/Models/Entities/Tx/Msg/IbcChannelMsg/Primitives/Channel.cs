using System.Collections.Generic;
using ProtoBuf;

namespace Terra.Sdk.Lcd.Models.Entities.Tx.Msg.IbcChannelMsg.Primitives
{
    [ProtoContract]
    public class Channel
    {
        [ProtoMember(1, Name = "state")] public State State { get; set; }
        [ProtoMember(2, Name = "order")] public Order Ordering { get; set; }
        [ProtoMember(3, Name = "counterparty")] public Counterparty Counterparty { get; set; }
        [ProtoMember(4, Name = "connection_hops")] public List<string> ConnectionHops { get; set; }
        [ProtoMember(5, Name = "version")] public string Version { get; set; }
    }
}
