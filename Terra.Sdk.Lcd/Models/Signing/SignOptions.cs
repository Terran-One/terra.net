using Terra.Sdk.Lcd.Models.Entities.Tx;

namespace Terra.Sdk.Lcd.Models.Signing
{
    public class SignOptions
    {
        public long AccountNumber { get; set; }
        public long Sequence { get; set; }
        public ModeInfo.SignMode SignMode { get; set; }
        public string ChainId { get; set; }
    }
}