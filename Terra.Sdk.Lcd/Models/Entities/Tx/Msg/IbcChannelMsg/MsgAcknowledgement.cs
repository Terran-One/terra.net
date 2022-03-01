using Newtonsoft.Json;
using Terra.Sdk.Lcd.Models.Entities.Tx.Msg.IbcChannelMsg.Primitives;

namespace Terra.Sdk.Lcd.Models.Entities.Tx.Msg.IbcChannelMsg
{
    public class MsgAcknowledgement : Msg
    {
        public Packet Packet { get; set; }
        public string Acknowledgement { get; set; }
        public string ProofAcked { get; set; }
        public Height ProofHeight { get; set; }
        public string Signer { get; set; }
    }
}
