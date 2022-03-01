using Terra.Sdk.Lcd.Models.Entities.Tx.Msg.IbcChannelMsg.Primitives;

namespace Terra.Sdk.Lcd.Models.Entities.Tx.Msg.IbcChannelMsg
{
    public class MsgChannelOpenTry : Msg
    {
        public string PortId { get; set; }
        public string PreviousChannelId { get; set; }
        public Channel Channel { get; set; }
        public string CounterpartyVersion { get; set; }
        public string ProofInit { get; set; }
        public Height ProofHeight { get; set; }
        public string Signer { get; set; }
    }
}
