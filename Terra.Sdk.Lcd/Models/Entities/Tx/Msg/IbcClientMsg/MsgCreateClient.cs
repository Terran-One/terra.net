using Newtonsoft.Json.Linq;

namespace Terra.Sdk.Lcd.Models.Entities.Tx.Msg.IbcClientMsg
{
    public class MsgCreateClient : Msg
    {
        public JObject ClientState { get; set; }
        public JObject ConsensusState { get; set; }
        public string Signer { get; set; }
    }
}
