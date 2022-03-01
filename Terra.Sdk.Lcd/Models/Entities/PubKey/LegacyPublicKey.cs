using System.Collections.Generic;

namespace Terra.Sdk.Lcd.Models.Entities.PubKey
{
    public class LegacyPublicKey : PubKey
    {
        public string Threshold { get; set; }

        public List<SimplePublicKey> PublicKeys { get; set; }
    }
}
