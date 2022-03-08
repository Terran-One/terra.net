using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Terra.Sdk.Lcd.Models.Entities.Tx.Msg.IbcChannelMsg.Primitives;
using Terra.Sdk.Lcd.Models.Entities.Tx.Msg.IbcConnectionMsg.Primitives;
using ProtoBuf;

namespace Terra.Sdk.Lcd.Models.Entities.Tx.Msg.IbcConnectionMsg
{
    [ProtoContract]
    public class MsgConnectionOpenTry : Msg
    {
        [ProtoMember(1)] public string ClientId { get; set; }
        [ProtoMember(2)] public string PreviousConnectionId { get; set; }
        [ProtoMember(3)] public JObject ClientState { get; set; }
        [ProtoMember(4)] public Counterparty Counterparty { get; set; }
        [ProtoMember(5)] public int DelayPeriod { get; set; }
        [ProtoMember(6)] public List<Version> CounterpartyVersions { get; set; }
        [ProtoMember(7)] public Height ProofHeight { get; set; }
        [ProtoMember(8)] public string ProofInit { get; set; }
        [ProtoMember(9)] public string ProofClient { get; set; }
        [ProtoMember(10)] public string ProofConsensus { get; set; }
        [ProtoMember(11)] public Height ConsensusHeight { get; set; }
        [ProtoMember(12)] public string Signer { get; set; }
    }
}