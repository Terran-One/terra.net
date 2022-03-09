using Terra.Sdk.Lcd.Models.Entities.PubKey;
using Terra.Sdk.Lcd.Models.Entities.Tx;

namespace Terra.Sdk.Lcd.Models.Signing
{
    public class SignatureV2
    {
        public PublicKey PublicKey { get; set; }
        public Descriptor Data { get; set; }
        public long Sequence { get; set; }
    }
}
