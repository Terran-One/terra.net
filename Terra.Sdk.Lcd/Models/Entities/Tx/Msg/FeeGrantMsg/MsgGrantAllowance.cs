using ProtoBuf;

namespace Terra.Sdk.Lcd.Models.Entities.Tx.Msg.FeeGrantMsg
{
    [ProtoContract]
    public class MsgGrantAllowance : Msg
    {
        [ProtoMember(1, Name = "granter")] public string Granter { get; set; }
        [ProtoMember(2, Name = "grantee")] public string Grantee { get; set; }
        [ProtoMember(3, Name = "allowance")] public Models.Entities.Allowance.Allowance Allowance { get; set; }
    }
}
