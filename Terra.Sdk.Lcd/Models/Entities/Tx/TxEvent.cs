using System.Collections.Generic;

namespace Terra.Sdk.Lcd.Models.Entities.Tx
{
    public class TxEvent
    {
        public string Type { get; set; }
        public List<TxAttribute> Attributes { get; set; }

        public class TxAttribute
        {
            public string Key { get; set; }
            public string Value { get; set; }
        }
    }
}