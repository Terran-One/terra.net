namespace Terra.Sdk.Lcd.Models.Entities.Deposit
{
    public enum ProposalStatus {
        Unspecified = 0,
        DepositPeriod = 1,
        VotingPeriod = 2,
        Passed = 3,
        Rejected = 4,
        Failed = 5,
        Unrecognized = -1
    }
}