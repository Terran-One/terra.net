using System.Collections.Generic;

namespace Terra.Sdk.Lcd.Models.Entities.Tx.Msg.MsgAuthMsg
{
    public class MsgExecAuthorized : Msg
    {
        public string Grantee { get; set; }
        public List<Msg> Msgs { get; set; }
    }
}
