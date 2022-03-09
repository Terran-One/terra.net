using ProtoBuf;
using Terra.Sdk.Lcd.Models.Entities.Tx;

namespace Terra.Sdk.Lcd.Models.Signing
{
    [ProtoContract]
    public class Single
    {
        [ProtoMember(1, Name = "mode")] public SignMode Mode { get; set; }
        [ProtoMember(2, Name = "signature")] public string Signature { get; set; }
    }
}