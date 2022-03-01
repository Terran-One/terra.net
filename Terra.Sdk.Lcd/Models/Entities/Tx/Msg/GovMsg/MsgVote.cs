using Terra.Sdk.Lcd.Models.Entities.Tx.Msg.GovMsg.Primitives;

namespace Terra.Sdk.Lcd.Models.Entities.Tx.Msg.GovMsg
{
    public class MsgVote : Msg
    {
        public long ProposalId { get; set; }
        public string Voter { get; set; }
        public VoteOption Option { get; set; }
    }
}
