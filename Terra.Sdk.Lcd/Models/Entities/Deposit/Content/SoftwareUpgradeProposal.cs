namespace Terra.Sdk.Lcd.Models.Entities.Deposit.Content
{
    public class SoftwareUpgradeProposal : Content
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public Plan Plan { get; set; }
    }
}