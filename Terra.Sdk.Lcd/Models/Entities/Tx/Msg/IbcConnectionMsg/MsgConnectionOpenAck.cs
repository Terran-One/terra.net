using System;
using Newtonsoft.Json.Linq;
using Terra.Sdk.Lcd.Models.Entities.Tx.Msg.IbcChannelMsg.Primitives;
using ProtoBuf;

namespace Terra.Sdk.Lcd.Models.Entities.Tx.Msg.IbcConnectionMsg
{
    [ProtoContract]
    public class MsgConnectionOpenAck : Msg
    {
        [ProtoMember(1)] public string ConnectionId { get; set; }
        [ProtoMember(2)] public string CounterpartyConnectionId { get; set; }

        [ProtoMember(3)]
        public Terra.Sdk.Lcd.Models.Entities.Tx.Msg.IbcConnectionMsg.Primitives.Version Version { get; set; }

        [ProtoMember(4)] public JObject ClientState { get; set; }
        [ProtoMember(5)] public Height ProofHeight { get; set; }
        [ProtoMember(6)] public string ProofTry { get; set; }
        [ProtoMember(7)] public string ProofClient { get; set; }
        [ProtoMember(8)] public string ProofConsensus { get; set; }
        [ProtoMember(9)] public Height ConsensusHeight { get; set; }
        [ProtoMember(10)] public string Signer { get; set; }
    }
}