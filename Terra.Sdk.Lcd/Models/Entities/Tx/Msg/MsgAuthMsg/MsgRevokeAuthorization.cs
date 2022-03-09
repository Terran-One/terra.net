using ProtoBuf;

namespace Terra.Sdk.Lcd.Models.Entities.Tx.Msg.MsgAuthMsg
{
    [ProtoContract]
    public class MsgRevokeAuthorization : Msg
    {
        protected override System.Type Type => typeof(MsgRevokeAuthorization);

        [ProtoMember(1, Name = "granter")] public string Granter { get; set; }
        [ProtoMember(2, Name = "grantee")] public string Grantee { get; set; }
        [ProtoMember(3, Name = "msg_type_url")] public string MsgTypeUrl { get; set; }
    }
}
