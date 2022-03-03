using Terra.Sdk.Lcd.Models.Entities.PubKey;
using Terra.Sdk.Lcd.Models.Entities.Tx;

namespace Terra.Sdk.Lcd.Models.Signing
{
    public class SignatureV2
    {
        public PublicKey PublicKey { get; set; }
        public ModeInfo Data { get; set; }
        public long Sequence { get; set; }
    }
}
