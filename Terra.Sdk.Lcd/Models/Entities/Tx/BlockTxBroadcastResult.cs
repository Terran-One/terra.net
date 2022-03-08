using System;
using System.Collections.Generic;

namespace Terra.Sdk.Lcd.Models.Entities.Tx
{
    public class BlockTxBroadcastResult
    {
        public string Height { get; set; }
        public string Txhash { get; set; }
        public string RawLog { get; set; }
        public string GasWanted { get; set; }
        public string GasUsed { get; set; }
        public List<TxLog> Logs { get; set; }
        public string Code { get; set; }
        public string Codespace { get; set; }
        public string Info { get; set; }
        public string Data { get; set; }
        public DateTime Timestamp { get; set; }
    }
}