using System.Collections.Generic;
using System.Linq;

namespace Terra.Sdk.Lcd.Models.Entities.Tx
{
    public class TxBody
    {
        public List<Msg.Msg> Messages { get; set; }
        public string Memo { get; set; }
        public long TimeoutHeight { get; set; }
    }
}
