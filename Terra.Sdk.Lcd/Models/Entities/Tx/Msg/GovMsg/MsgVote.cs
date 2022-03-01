namespace Terra.Sdk.Lcd.Models.Entities.Tx.Msg.GovMsg
{
    public class MsgVote : Msg
    {
        public long ProposalId { get; set; }
        public string Voter { get; set; }
        public VoteOption Option { get; set; }

        public enum VoteOption
        {
            VoteOptionUnspecified = 0,
            VoteOptionYes = 1,
            VoteOptionAbstain = 2,
            VoteOptionNo = 3,
            VoteOptionNoWithVeto = 4,
            Unrecognized = -1
        }
    }
}
