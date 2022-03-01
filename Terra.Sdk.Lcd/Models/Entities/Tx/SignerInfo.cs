using System.Security.Cryptography.X509Certificates;
using Newtonsoft.Json;

namespace Terra.Sdk.Lcd.Models.Entities.Tx
{
    public class SignerInfo
    {
        [JsonProperty("public_key")]
        public PublicKey PublicKey { get; set; }

        [JsonProperty("sequence")]
        public long Sequence { get; set; }

        [JsonProperty("mode_info")]
        public ModeInfo ModeInfo { get; set; }
    }
}