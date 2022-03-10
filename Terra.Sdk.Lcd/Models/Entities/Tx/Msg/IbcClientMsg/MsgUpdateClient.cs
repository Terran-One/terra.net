using Newtonsoft.Json;
using ProtoBuf;

namespace Terra.Sdk.Lcd.Models.Entities.Tx.Msg.IbcClientMsg
{
    [ProtoContract]
    public class MsgUpdateClient : Msg
    {
        protected override System.Type Type => typeof(MsgUpdateClient);

        [ProtoMember(1, Name = "client_id")]
        public string ClientId { get; set; }

        [JsonIgnore]
        [ProtoMember(2, Name = "header")]
        public Any ProtoHeader
        {
            get => _protoHeader;
            set
            {
                _protoHeader = value;
                _header = value.DecodeDynamic();
            }
        }
        private Any _protoHeader;

        public dynamic Header
        {
            get => _header;
            set
            {
                _header = value;
                _protoHeader = Any.EncodeDynamic(value);
            }
        }
        private dynamic _header;

        [ProtoMember(3, Name = "signer")]
        public string Signer { get; set; }
    }
}
