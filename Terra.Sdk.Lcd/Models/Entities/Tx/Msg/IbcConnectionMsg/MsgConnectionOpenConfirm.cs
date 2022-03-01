using Terra.Sdk.Lcd.Models.Entities.Tx.Msg.IbcChannelMsg.Primitives;

namespace Terra.Sdk.Lcd.Models.Entities.Tx.Msg.IbcConnectionMsg
{
    public class MsgConnectionOpenConfirm : Msg
    {
        public string ConnectionId { get; set; }
        public string ProofAck { get; set; }
        public Height ProofHeight { get; set; }
        public string Signer { get; set; }
    }
}
