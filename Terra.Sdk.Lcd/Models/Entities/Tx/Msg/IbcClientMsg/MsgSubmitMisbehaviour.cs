using Newtonsoft.Json.Linq;

namespace Terra.Sdk.Lcd.Models.Entities.Tx.Msg.IbcClientMsg
{
    public class MsgSubmitMisbehaviour : Msg
    {
        public string ClientId { get; set; }
        public JObject Misbehaviour { get; set; }
        public string Signer { get; set; }
    }
}
