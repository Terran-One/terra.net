using ProtoBuf;

namespace Terra.Sdk.Lcd.Models.Entities.Tx.Msg.FeeGrantMsg
{
    [ProtoContract]
    public class MsgGrantAllowance : Msg
    {
        [ProtoMember(1)] public string Granter { get; set; }
        [ProtoMember(2)] public string Grantee { get; set; }
        [ProtoMember(3)] public Models.Entities.Allowance.Allowance Allowance { get; set; }
    }
}