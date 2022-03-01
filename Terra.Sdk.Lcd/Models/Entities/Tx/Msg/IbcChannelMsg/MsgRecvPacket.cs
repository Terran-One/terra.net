using Terra.Sdk.Lcd.Models.Entities.Tx.Msg.IbcChannelMsg.Primitives;

namespace Terra.Sdk.Lcd.Models.Entities.Tx.Msg.IbcChannelMsg
{
    public class MsgRecvPacket : Msg
    {
        public Packet Packet { get; set; }
        public string ProofCommitment { get; set; }
        public Height ProofHeight { get; set; }
        public string Signer { get; set; }
    }
}
