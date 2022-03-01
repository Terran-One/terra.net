using System.Collections.Generic;

namespace Terra.Sdk.Lcd.Models.Entities.Tx
{
    public class TxSearchResult
    {
        public List<Tx> Txs { get; set; }
        public List<TxInfo> TxResponses { get; set; }
    }
}
