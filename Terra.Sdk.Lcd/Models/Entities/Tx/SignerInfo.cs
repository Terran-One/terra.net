using System.Security.Cryptography.X509Certificates;

namespace Terra.Sdk.Lcd.Models.Entities.Tx
{
    public class SignerInfo
    {
        public PublicKey PublicKey { get; set; }
        public long Sequence { get; set; }
        public ModeInfo ModeInfo { get; set; }
    }
}
