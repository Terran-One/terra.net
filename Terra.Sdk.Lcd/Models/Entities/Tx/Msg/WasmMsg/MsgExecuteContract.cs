using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using ProtoBuf;

namespace Terra.Sdk.Lcd.Models.Entities.Tx.Msg.WasmMsg
{
    [ProtoContract]
    public class MsgExecuteContract : Msg
    {
        [ProtoMember(1)] public string Sender { get; set; }
        [ProtoMember(2)] public string Contract { get; set; }
        [ProtoMember(3)] public JObject ExecuteMsg { get; set; }
        [ProtoMember(4)] public List<Coin> Coins { get; set; }
    }
}