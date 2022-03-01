using System;
using Newtonsoft.Json.Linq;
using Terra.Sdk.Lcd.Models.Entities.Tx.Msg.IbcChannelMsg.Primitives;

namespace Terra.Sdk.Lcd.Models.Entities.Tx.Msg.IbcConnectionMsg
{
    public class MsgConnectionOpenAck : Msg
    {
        public string ConnectionId { get; set; }
        public string CounterpartyConnectionId { get; set; }
        public Version Version { get; set; }
        public JObject ClientState { get; set; }
        public Height ProofHeight { get; set; }
        public string ProofTry { get; set; }
        public string ProofClient { get; set; }
        public string ProofConsensus { get; set; }
        public Height ConsensusHeight { get; set; }
        public string Signer { get; set; }
    }
}
