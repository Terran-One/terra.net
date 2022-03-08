using Newtonsoft.Json.Linq;
using ProtoBuf;

namespace Terra.Sdk.Lcd.Models.Entities.Tx.Msg.WasmMsg
{
    [ProtoContract]
    public class MsgMigrateContract : Msg
    {
        [ProtoMember(1)] public string Admin { get; set; }
        [ProtoMember(2)] public string Contract { get; set; }
        [ProtoMember(3)] public long NewCodeId { get; set; }
        [ProtoMember(4)] public JObject MigrateMsg { get; set; }
    }
}