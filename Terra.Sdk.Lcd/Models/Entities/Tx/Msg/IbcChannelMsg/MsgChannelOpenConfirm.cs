using Terra.Sdk.Lcd.Models.Entities.Tx.Msg.IbcChannelMsg.Primitives;

namespace Terra.Sdk.Lcd.Models.Entities.Tx.Msg.IbcChannelMsg
{
    public class MsgChannelOpenConfirm : Msg
    {
        public string PortId { get; set; }
        public string ChannelId { get; set; }
        public string ProofAck { get; set; }
        public Height ProofHeight { get; set; }
        public string Signer { get; set; }
    }
}
