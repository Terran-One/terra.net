using System.Collections.Generic;
using Newtonsoft.Json;
using Terra.Sdk.Lcd.Models.Entities.Tx.Msg.IbcChannelMsg.Primitives;
using Terra.Sdk.Lcd.Models.Entities.Tx.Msg.IbcConnectionMsg.Primitives;
using ProtoBuf;

namespace Terra.Sdk.Lcd.Models.Entities.Tx.Msg.IbcConnectionMsg
{
    [ProtoContract]
    public class MsgConnectionOpenTry : Msg
    {
        protected override System.Type Type => typeof(MsgConnectionOpenTry);

        [ProtoMember(1, Name = "client_id")]
        public string ClientId { get; set; }

        [ProtoMember(2, Name = "previous_connection_id")]
        public string PreviousConnectionId { get; set; }

        [JsonIgnore]
        [ProtoMember(3, Name = "client_state")]
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
                _protoClientState = Any.EncodeDynamic(value);
            }
        }
        private dynamic _clientState;

        [ProtoMember(4, Name = "counterparty")]
        public Counterparty Counterparty { get; set; }

        [ProtoMember(5, Name = "delay_period")]
        public int DelayPeriod { get; set; }

        [ProtoMember(6, Name = "counterparty_versions")]
        public List<Version> CounterpartyVersions { get; set; }

        [ProtoMember(7, Name = "proof_height")]
        public Height ProofHeight { get; set; }

        [ProtoMember(8, Name = "proof_init")]
        public string ProofInit { get; set; }

        [ProtoMember(9, Name = "proof_client")]
        public string ProofClient { get; set; }

        [ProtoMember(10, Name = "proof_consensus")]
        public string ProofConsensus { get; set; }

        [ProtoMember(11, Name = "consensus_height")]
        public Height ConsensusHeight { get; set; }

        [ProtoMember(12, Name = "signer")]
        public string Signer { get; set; }
    }
}
