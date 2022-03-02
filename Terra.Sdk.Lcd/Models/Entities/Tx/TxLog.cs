using System.Collections.Generic;

namespace Terra.Sdk.Lcd.Models.Entities.Tx
{
    public class TxLog
    {
        public long MsgIndex { get; set; }
        public string Log { get; set; }
        public List<TxEvent> Events { get; set; }
    }
}
