using Terra.Sdk.Lcd.Models.Entities.PubKey;

namespace Terra.Sdk.Lcd.Models.Signing
{
    public class SignatureV2
    {
        public PublicKey PublicKey { get; set; }
        public Descriptor Data { get; set; }
        public long Sequence { get; set; }
    }
}
