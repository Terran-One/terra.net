using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Terra.Sdk.Lcd.Models.Entities.Tx
{
    public class TxInfo
    {
        public string Height { get; set; }
        [JsonProperty("txhash")]
        public string TxHash { get; set; }
        [JsonProperty("codespace")]
        public string CodeSpace { get; set; }
        public string Code { get; set; }
        public string Data { get; set; }
        public string RawLog { get; set; }
        public List<TxLog> Logs { get; set; }
        public string Info { get; set; }
        public string GasWanted { get; set; }
        public string GasUsed { get; set; }
        public Tx Tx { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
