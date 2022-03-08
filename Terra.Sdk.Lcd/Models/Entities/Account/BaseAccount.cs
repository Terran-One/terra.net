using Terra.Sdk.Lcd.Models.Entities.PubKey;

namespace Terra.Sdk.Lcd.Models.Entities.Account
{
    public class BaseAccount : Account
    {
        public string Address { get; set; }

        public PublicKey PubKey { get; set; }

        public long AccountNumber { get; set; }

        public long Sequence { get; set; }
    }
}