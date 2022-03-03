namespace Terra.Sdk.Lcd.Models.Entities.Staking
{
    public class RedelegationDetails
    {
        public string DelegatorAddress { get; set; }
        public string ValidatorSrcAddress { get; set; }
        public string ValidatorDstAddress { get; set; }
    }
}