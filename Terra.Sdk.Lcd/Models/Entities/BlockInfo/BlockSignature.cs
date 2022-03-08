namespace Terra.Sdk.Lcd.Models.Entities.BlockInfo
{
    public class BlockSignature
    {
        public BlockIdFlag BlockIdFlag { get; set; }
        public string ValidatorAddress { get; set; }
        public string Timestamp { get; set; }
        public string Signature { get; set; }
    }
}
