using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace Terra.Sdk.Lcd.Models.Entities.Tx.Msg.WasmMsg
{
    public class MsgExecuteContract : Msg
    {
        public string Sender { get; set; }
        public string Contract { get; set; }
        public JObject ExecuteMsg { get; set; }
        public List<Coin> Coins { get; set; }
    }
}
