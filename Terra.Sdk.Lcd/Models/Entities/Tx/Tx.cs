using System.Collections.Generic;

namespace Terra.Sdk.Lcd.Models.Entities.Tx
{
    public class Tx
    {
        public TxBody Body { get; set; }
        public AuthInfo AuthInfo { get; set; }
        public List<string> Signatures { get; set; }
    }
}
