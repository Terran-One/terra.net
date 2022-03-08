using ProtoBuf;

namespace Terra.Sdk.Lcd.Models.Entities.Tx.Msg.FeeGrantMsg
{
    [ProtoContract]
    public class MsgRevokeAllowance : Msg
    {
        [ProtoMember(1)] public string Granter { get; set; }
        [ProtoMember(2)] public string Grantee { get; set; }
    }
}