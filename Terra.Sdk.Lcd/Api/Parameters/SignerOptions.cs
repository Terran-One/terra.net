using Terra.Sdk.Lcd.Models.Entities.PubKey;

namespace Terra.Sdk.Lcd.Api.Parameters
{
    public class SignerOptions
    {
        public string Address { get; set; }
        public long? SequenceNumber { get; set; }
        public PublicKey PublicKey { get; set; }
    }
}