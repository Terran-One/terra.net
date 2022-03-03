namespace Terra.Sdk.Lcd.Models.Entities.Staking
{
    public enum BondStatus
    {
        Unspecified = 0,
        Unbonded = 1,
        Unbonding = 2,
        Bonded = 3,
        Unrecognized = -1
    }
}