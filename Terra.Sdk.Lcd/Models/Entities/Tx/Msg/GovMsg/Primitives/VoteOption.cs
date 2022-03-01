namespace Terra.Sdk.Lcd.Models.Entities.Tx.Msg.GovMsg.Primitives
{
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
