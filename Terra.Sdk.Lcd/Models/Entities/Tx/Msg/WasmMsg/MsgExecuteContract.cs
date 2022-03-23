using System.Collections.Generic;
using Newtonsoft.Json;
using ProtoBuf;

namespace Terra.Sdk.Lcd.Models.Entities.Tx.Msg.WasmMsg
{
    [ProtoContract]
    public class MsgExecuteContract : Msg
    {
        protected override System.Type Type => typeof(MsgExecuteContract);

        [ProtoMember(1, Name = "sender")]
        public string Sender { get; set; }

        [ProtoMember(2, Name = "contract")]
        public string Contract { get; set; }

        [JsonIgnore]
        [ProtoMember(3, Name = "execute_msg")]
        public Any ProtoExecuteMsg
        {
            get => _protoExecuteMsg;
            set
            {
                _protoExecuteMsg = value;
                _executeMsg = value.DecodeDynamic();
            }
        }
        private Any _protoExecuteMsg;

        public dynamic ExecuteMsg
        {
            get => _executeMsg;
            set
            {
                _executeMsg = value;
                _protoExecuteMsg = new Any {Value = Any.EncodeDynamic(value)};
            }
        }
        private dynamic _executeMsg;

        [ProtoMember(4, Name = "coins")]
        public List<Coin> Coins { get; set; }
    }
}
