using System.Collections.Generic;
using System.Linq;

namespace Terra.Sdk.Lcd.Models.Entities.PubKey
{
    public class LegacyAminoMultisigPublicKey : PublicKey
    {
        public string Threshold { get; set; }

        public override string Key
        {
            get => PublicKeys?.FirstOrDefault()?.Key;
            set {}
        }

        public List<SimplePublicKey> PublicKeys { get; set; }
    }
}
