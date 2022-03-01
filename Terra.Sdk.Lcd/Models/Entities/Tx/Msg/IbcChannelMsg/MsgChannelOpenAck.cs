using Terra.Sdk.Lcd.Models.Entities.Tx.Msg.IbcChannelMsg.Primitives;

namespace Terra.Sdk.Lcd.Models.Entities.Tx.Msg.IbcChannelMsg
{
    public class MsgChannelOpenAck : Msg
    {
        public string PortId { get; set; }
        public string ChannelId { get; set; }
        public string CounterpartyChannelId { get; set; }
        public string CounterpartyVersion { get; set; }
        public string ProofTry { get; set; }
        public Height ProofHeight { get; set; }
        public string Signer { get; set; }
    }
}
