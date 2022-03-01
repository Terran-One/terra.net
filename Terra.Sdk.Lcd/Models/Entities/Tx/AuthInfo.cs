using System.Collections.Generic;
using Newtonsoft.Json;

namespace Terra.Sdk.Lcd.Models.Entities.Tx
{
    public class AuthInfo
    {
        [JsonProperty("signer_infos")]
        public List<SignerInfo> SignerInfos { get; set; }

        [JsonProperty("fee")]
        public Fee Fee { get; set; }
    }
}