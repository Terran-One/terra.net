using Newtonsoft.Json;
using Terra.Sdk.Lcd.Models.Entities.Tx.Msg.IbcChannelMsg.Primitives;
using ProtoBuf;

namespace Terra.Sdk.Lcd.Models.Entities.Tx.Msg.IbcConnectionMsg
{
    [ProtoContract]
    public class MsgConnectionOpenAck : Msg
    {
        protected override System.Type Type => typeof(MsgConnectionOpenAck);

        [ProtoMember(1, Name = "connection_id")]
        public string ConnectionId { get; set; }

        [ProtoMember(2, Name = "counterparty_connection_id")]
        public string CounterpartyConnectionId { get; set; }

        [ProtoMember(3, Name = "version")]
        public Primitives.Version Version { get; set; }

        [JsonIgnore]
        [ProtoMember(4, Name = "client_state")]
        public Any ProtoClientState
        {
            get => _protoClientState;
            set
            {
                _protoClientState = value;
                _clientState = value.DecodeDynamic();
            }
        }
        private Any _protoClientState;

        public dynamic ClientState
        {
            get => _clientState;
            set
            {
                _clientState = value;
                _protoClientState = new Any {Value = Any.EncodeDynamic(value)};
            }
        }
        private dynamic _clientState;

        [ProtoMember(5, Name = "proof_height")]
        public Height ProofHeight { get; set; }

        [ProtoMember(6, Name = "proof_try")]
        public string ProofTry { get; set; }

        [ProtoMember(7, Name = "proof_client")]
        public string ProofClient { get; set; }

        [ProtoMember(8, Name = "proof_consensus")]
        public string ProofConsensus { get; set; }

        [ProtoMember(9, Name = "consensus_height")]
        public Height ConsensusHeight { get; set; }

        [ProtoMember(10, Name = "signer")]
        public string Signer { get; set; }
    }
}
