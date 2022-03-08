using System.Collections.Generic;
using Newtonsoft.Json.Linq;

using ProtoBuf; namespace Terra.Sdk.Lcd.Models.Entities.Tx.Msg.WasmMsg
{
    [ProtoContract]public class MsgInstantiateContract : Msg
    {
        [ProtoMember(1)]public string Sender { get; set; }
        [ProtoMember(2)]public string Admin { get; set; }
        [ProtoMember(3)]public long CodeId { get; set; }
        [ProtoMember(4)]public JObject InitMsg { get; set; }
        [ProtoMember(5)]public List<Coin> InitCoins { get; set; }
    }
}
