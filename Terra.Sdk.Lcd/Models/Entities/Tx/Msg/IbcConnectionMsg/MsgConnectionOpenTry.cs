using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Terra.Sdk.Lcd.Models.Entities.Tx.Msg.IbcChannelMsg.Primitives;
using Terra.Sdk.Lcd.Models.Entities.Tx.Msg.IbcConnectionMsg.Primitives;

namespace Terra.Sdk.Lcd.Models.Entities.Tx.Msg.IbcConnectionMsg
{
    public class MsgConnectionOpenTry : Msg
    {
        public string ClientId { get; set; }
        public string PreviousConnectionId { get; set; }
        public JObject ClientState { get; set; }
        public Counterparty Counterparty { get; set; }
        public int DelayPeriod { get; set; }
        public List<Version> CounterpartyVersions { get; set; }
        public Height ProofHeight { get; set; }
        public string ProofInit { get; set; }
        public string ProofClient { get; set; }
        public string ProofConsensus { get; set; }
        public Height ConsensusHeight { get; set; }
        public string Signer { get; set; }
    }
}
