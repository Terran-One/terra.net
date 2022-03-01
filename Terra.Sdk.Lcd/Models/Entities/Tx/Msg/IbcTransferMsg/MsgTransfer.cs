using System;
using Terra.Sdk.Lcd.Models.Entities.Tx.Msg.IbcChannelMsg.Primitives;

namespace Terra.Sdk.Lcd.Models.Entities.Tx.Msg.IbcTransferMsg
{
    public class MsgTransfer : Msg
    {
        public string SourcePort { get; set; }
        public string SourceChannel { get; set; }
        public Coin Token { get; set; }
        public string Sender { get; set; }
        public string Receiver { get; set; }
        public Height TimeoutHeight { get; set; }
        public DateTime TimeoutTimestamp { get; set; }
    }
}
