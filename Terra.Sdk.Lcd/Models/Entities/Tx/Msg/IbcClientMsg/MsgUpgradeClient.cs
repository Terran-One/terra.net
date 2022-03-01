using Newtonsoft.Json.Linq;

namespace Terra.Sdk.Lcd.Models.Entities.Tx.Msg.IbcClientMsg
{
    public class MsgUpgradeClient : Msg
    {
        public string ClientId { get; set; }
        public JObject ClientState { get; set; }
        public JObject ConsensusState { get; set; }
        public string ProofUpgradeClient { get; set; }
        public string ProofUpgradeConsensusState { get; set; }
        public string Signer { get; set; }
    }
}
