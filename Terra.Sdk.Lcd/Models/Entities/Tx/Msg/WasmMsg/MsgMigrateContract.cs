using Newtonsoft.Json.Linq;
using ProtoBuf;

namespace Terra.Sdk.Lcd.Models.Entities.Tx.Msg.WasmMsg
{
    [ProtoContract]
    public class MsgMigrateContract : Msg
    {
        protected override System.Type Type => typeof(MsgMigrateContract);

        [ProtoMember(1, Name = "admin")] public string Admin { get; set; }
        [ProtoMember(2, Name = "contract")] public string Contract { get; set; }
        [ProtoMember(3, Name = "new_code_id")] public long NewCodeId { get; set; }
        [ProtoMember(4, Name = "migrate_msg")] public JObject MigrateMsg { get; set; }
    }
}
