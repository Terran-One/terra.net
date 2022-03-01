using Terra.Sdk.Lcd.Models.Entities.Tx.Msg.IbcChannelMsg.Primitives;

namespace Terra.Sdk.Lcd.Models.Entities.Tx.Msg.IbcChannelMsg
{
    public class MsgChannelCloseConfirm : Msg
    {
        public string PortId { get; set; }
        public string ChannelId { get; set; }
        public string ProofInit { get; set; }
        public Height ProofHeight { get; set; }
        public string Signer { get; set; }
    }
}
