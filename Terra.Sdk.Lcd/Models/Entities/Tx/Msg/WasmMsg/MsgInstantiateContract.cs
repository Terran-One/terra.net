using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace Terra.Sdk.Lcd.Models.Entities.Tx.Msg.WasmMsg
{
    public class MsgInstantiateContract : Msg
    {
        public string Sender { get; set; }
        public string Admin { get; set; }
        public long CodeId { get; set; }
        public JObject InitMsg { get; set; }
        public List<Coin> InitCoins { get; set; }
    }
}
