using Newtonsoft.Json;
using ProtoBuf;

namespace Terra.Sdk.Lcd.Models.Entities.Tx.Msg.IbcClientMsg
{
    [ProtoContract]
    public class MsgCreateClient : Msg
    {
        protected override System.Type Type => typeof(MsgCreateClient);

        [JsonIgnore]
        [ProtoMember(1, Name = "client_state")]
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

        [JsonIgnore]
        [ProtoMember(2, Name = "consensus_state")]
        public Any ProtoConsensusState
        {
            get => _protoConsensusState;
            set
            {
                _protoConsensusState = value;
                _consensusState = value.DecodeDynamic();
            }
        }
        private Any _protoConsensusState;

        public dynamic ConsensusState
        {
            get => _consensusState;
            set
            {
                _consensusState = value;
                _protoConsensusState = Any.EncodeDynamic(value);
            }
        }
        private dynamic _consensusState;

        [ProtoMember(3, Name = "signer")]
        public string Signer { get; set; }
    }
}
