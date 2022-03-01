using System.Collections.Generic;

namespace Terra.Sdk.Lcd.Models.Entities.Tx.Msg.IbcConnectionMsg.Primitives
{
    public class Version
    {
        public string Identifier { get; set; }
        public List<string> Features { get; set; }
    }
}
