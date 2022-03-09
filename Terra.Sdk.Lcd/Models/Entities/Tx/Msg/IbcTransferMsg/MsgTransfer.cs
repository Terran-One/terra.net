using System;
using Terra.Sdk.Lcd.Models.Entities.Tx.Msg.IbcChannelMsg.Primitives;
using ProtoBuf;

namespace Terra.Sdk.Lcd.Models.Entities.Tx.Msg.IbcTransferMsg
{
    [ProtoContract]
    public class MsgTransfer : Msg
    {
        [ProtoMember(1, Name = "source_port")] public string SourcePort { get; set; }
        [ProtoMember(2, Name = "source_channel")] public string SourceChannel { get; set; }
        [ProtoMember(3, Name = "token")] public Coin Token { get; set; }
        [ProtoMember(4, Name = "sender")] public string Sender { get; set; }
        [ProtoMember(5, Name = "receiver")] public string Receiver { get; set; }
        [ProtoMember(6, Name = "timeout_height")] public Height TimeoutHeight { get; set; }
        [ProtoMember(7, Name = "timeout_timestamp")] public DateTime TimeoutTimestamp { get; set; }
    }
}
