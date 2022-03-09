using ProtoBuf;

namespace Terra.Sdk.Lcd.Models.Entities.Tx.Msg.FeeGrantMsg
{
    [ProtoContract]
    public class MsgRevokeAllowance : Msg
    {
        [ProtoMember(1, Name = "granter")] public string Granter { get; set; }
        [ProtoMember(2, Name = "grantee")] public string Grantee { get; set; }
    }
}
