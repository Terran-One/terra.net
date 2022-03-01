using Newtonsoft.Json.Linq;

namespace Terra.Sdk.Lcd.Models.Entities.Tx.Msg.WasmMsg
{
    public class MsgMigrateContract : Msg
    {
        public string Admin { get; set; }
        public string Contract { get; set; }
        public long NewCodeId { get; set; }
        public JObject MigrateMsg { get; set; }
    }
}
