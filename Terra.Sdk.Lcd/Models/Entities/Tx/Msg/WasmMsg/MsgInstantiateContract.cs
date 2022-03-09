using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using ProtoBuf;

namespace Terra.Sdk.Lcd.Models.Entities.Tx.Msg.WasmMsg
{
    [ProtoContract]
    public class MsgInstantiateContract : Msg
    {
        [ProtoMember(1, Name = "sender")] public string Sender { get; set; }
        [ProtoMember(2, Name = "admin")] public string Admin { get; set; }
        [ProtoMember(3, Name = "code_id")] public long CodeId { get; set; }
        [ProtoMember(4, Name = "init_msg")] public JObject InitMsg { get; set; }
        [ProtoMember(5, Name = "init_coins")] public List<Coin> InitCoins { get; set; }
    }
}
