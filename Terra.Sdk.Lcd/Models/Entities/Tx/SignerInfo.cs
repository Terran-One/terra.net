using Newtonsoft.Json.Linq;

namespace Terra.Sdk.Lcd.Models.Entities.Tx
{
    public class SignerInfo
    {
        public JObject PublicKey { get; set; }
        public long Sequence { get; set; }
        public ModeInfo ModeInfo { get; set; }
    }
}
