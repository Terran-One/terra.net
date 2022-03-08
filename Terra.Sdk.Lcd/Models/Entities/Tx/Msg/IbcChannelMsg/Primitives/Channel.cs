using System.Collections.Generic;

using ProtoBuf; namespace Terra.Sdk.Lcd.Models.Entities.Tx.Msg.IbcChannelMsg.Primitives
{
    [ProtoContract]
    public class Channel
    {
        [ProtoMember(1)]public State State { get; set; }
        [ProtoMember(2)]public Order Ordering { get; set; }
        [ProtoMember(3)]public Counterparty Counterparty { get; set; }
        [ProtoMember(4)]public List<string> ConnectionHops { get; set; }
        [ProtoMember(5)]public string Version { get; set; }
    }
}
