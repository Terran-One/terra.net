using Newtonsoft.Json.Linq;

namespace Terra.Sdk.Lcd.Models.Entities.Tx.Msg.IbcClientMsg
{
    public class MsgUpdateClient : Msg
    {
        public string ClientId { get; set; }
        public JObject Header { get; set; }
        public string Signer { get; set; }
    }
}
