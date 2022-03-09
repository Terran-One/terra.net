using Terra.Sdk.Lcd.Models.Entities.Tx.Msg.MsgAuthMsg.Primitives;
using ProtoBuf;

namespace Terra.Sdk.Lcd.Models.Entities.Tx.Msg.MsgAuthMsg
{
    [ProtoContract]
    public class MsgGrantAuthorization : Msg
    {
        protected override System.Type Type => typeof(MsgGrantAuthorization);

        [ProtoMember(1, Name = "granter")] public string Granter { get; set; }
        [ProtoMember(2, Name = "grantee")] public string Grantee { get; set; }
        [ProtoMember(3, Name = "grant")] public AuthorizationGrant Grant { get; set; }
    }
}
