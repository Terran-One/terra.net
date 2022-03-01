using Terra.Sdk.Lcd.Models.Entities.Tx.Msg.IbcChannelMsg.Primitives;
using Terra.Sdk.Lcd.Models.Entities.Tx.Msg.IbcConnectionMsg.Primitives;

namespace Terra.Sdk.Lcd.Models.Entities.Tx.Msg.IbcConnectionMsg
{
    public class MsgConnectionOpenInit : Msg
    {
        public string ClientId { get; set; }
        public Counterparty Counterparty { get; set; }
        public Version Version { get; set; }
        public string DelayPeriod { get; set; }
        public string Signer { get; set; }
    }
}
