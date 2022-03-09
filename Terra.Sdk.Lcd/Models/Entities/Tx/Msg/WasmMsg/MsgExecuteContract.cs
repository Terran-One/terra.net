using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using ProtoBuf;

namespace Terra.Sdk.Lcd.Models.Entities.Tx.Msg.WasmMsg
{
    [ProtoContract]
    public class MsgExecuteContract : Msg
    {
        protected override System.Type Type => typeof(MsgExecuteContract);

        [ProtoMember(1, Name = "sender")] public string Sender { get; set; }
        [ProtoMember(2, Name = "contract")] public string Contract { get; set; }
        [ProtoMember(3, Name = "execute_msg")] public JObject ExecuteMsg { get; set; }
        [ProtoMember(4, Name = "coins")] public List<Coin> Coins { get; set; }
    }
}
