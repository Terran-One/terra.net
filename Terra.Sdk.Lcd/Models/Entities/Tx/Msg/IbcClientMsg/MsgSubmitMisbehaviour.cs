using Newtonsoft.Json;
using ProtoBuf;

namespace Terra.Sdk.Lcd.Models.Entities.Tx.Msg.IbcClientMsg
{
    [ProtoContract]
    public class MsgSubmitMisbehaviour : Msg
    {
        protected override System.Type Type => typeof(MsgSubmitMisbehaviour);

        [ProtoMember(1, Name = "client_id")]
        public string ClientId { get; set; }

        [JsonIgnore]
        [ProtoMember(2, Name = "misbehaviour")]
        public Any ProtoMisbehaviour
        {
            get => _protoMisbehaviour;
            set
            {
                _protoMisbehaviour = value;
                _misbehaviour = value.DecodeDynamic();
            }
        }
        private Any _protoMisbehaviour;

        public dynamic Misbehaviour
        {
            get => _misbehaviour;
            set
            {
                _misbehaviour = value;
                _protoMisbehaviour = new Any {Value = Any.EncodeDynamic(value)};
            }
        }
        private dynamic _misbehaviour;

        [ProtoMember(3, Name = "signer")]
        public string Signer { get; set; }
    }
}
