using System.Collections.Generic;

namespace Terra.Sdk.Lcd.Models.Entities.Tx
{
    public class AuthInfo
    {
        public List<SignerInfo> SignerInfos { get; set; }
        public Fee Fee { get; set; }
    }
}
