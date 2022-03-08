using System;
using Terra.Sdk.Lcd.Models.Entities.Tx.Msg.IbcChannelMsg.Primitives;
using ProtoBuf;

namespace Terra.Sdk.Lcd.Models.Entities.Tx.Msg.IbcTransferMsg
{
    [ProtoContract]
    public class MsgTransfer : Msg
    {
        [ProtoMember(1)] public string SourcePort { get; set; }
        [ProtoMember(2)] public string SourceChannel { get; set; }
        [ProtoMember(3)] public Coin Token { get; set; }
        [ProtoMember(4)] public string Sender { get; set; }
        [ProtoMember(5)] public string Receiver { get; set; }
        [ProtoMember(6)] public Height TimeoutHeight { get; set; }
        [ProtoMember(7)] public DateTime TimeoutTimestamp { get; set; }
    }
}