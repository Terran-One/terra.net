using ProtoBuf; namespace Terra.Sdk.Lcd.Models.Entities.Tx.Msg.MsgAuthMsg
{
    [ProtoContract]public class MsgRevokeAuthorization : Msg
    {
        [ProtoMember(1)]public string Granter { get; set; }
        [ProtoMember(2)]public string Grantee { get; set; }
        [ProtoMember(3)]public string MsgTypeUrl { get; set; }
    }
}
