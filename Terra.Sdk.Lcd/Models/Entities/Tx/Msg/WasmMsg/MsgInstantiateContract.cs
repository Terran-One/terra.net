using System.Collections.Generic;
using Newtonsoft.Json;
using ProtoBuf;

namespace Terra.Sdk.Lcd.Models.Entities.Tx.Msg.WasmMsg
{
    [ProtoContract]
    public class MsgInstantiateContract : Msg
    {
        protected override System.Type Type => typeof(MsgInstantiateContract);

        [ProtoMember(1, Name = "sender")]
        public string Sender { get; set; }

        [ProtoMember(2, Name = "admin")]
        public string Admin { get; set; }

        [ProtoMember(3, Name = "code_id")]
        public long CodeId { get; set; }

        [JsonIgnore]
        [ProtoMember(4, Name = "init_msg")]
        public Any ProtoInitMsg
        {
            get => _protoInitMsg;
            set
            {
                _protoInitMsg = value;
                _initMsg = value.DecodeDynamic();
            }
        }
        private Any _protoInitMsg;

        public dynamic InitMsg
        {
            get => _initMsg;
            set
            {
                _initMsg = value;
                _protoInitMsg = new Any {Value = Any.EncodeDynamic(value)};
            }
        }
        private dynamic _initMsg;

        [ProtoMember(5, Name = "init_coins")]
        public List<Coin> InitCoins { get; set; }
    }
}
