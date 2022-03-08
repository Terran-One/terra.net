using Terra.Sdk.Lcd.Models.Entities.Tx.Msg.MsgAuthMsg.Primitives;

using ProtoBuf; namespace Terra.Sdk.Lcd.Models.Entities.Tx.Msg.MsgAuthMsg
{
    [ProtoContract]public class MsgGrantAuthorization : Msg
    {
        [ProtoMember(1)]public string Granter { get; set; }
        [ProtoMember(2)]public string Grantee { get; set; }
        [ProtoMember(3)]public AuthorizationGrant Grant { get; set; }
    }
}
