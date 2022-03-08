using System.Collections.Generic;

using ProtoBuf; namespace Terra.Sdk.Lcd.Models.Entities.Tx.Msg.MsgAuthMsg
{
    [ProtoContract]public class MsgExecAuthorized : Msg
    {
        [ProtoMember(1)]public string Grantee { get; set; }
        [ProtoMember(2)]public List<Msg> Msgs { get; set; }
    }
}
