namespace Terra.Sdk.Lcd.Models.Entities.Staking
{
    public class RedelegationEntryDetails
    {
        public long CreationHeight { get; set; }
        public string CompletionTime { get; set; }
        public string InitialBalance { get; set; }
        public string SharesDst { get; set; }
    }
}