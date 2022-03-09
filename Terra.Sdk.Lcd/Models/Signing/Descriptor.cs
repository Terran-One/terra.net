using ProtoBuf;

namespace Terra.Sdk.Lcd.Models.Signing
{
    [ProtoContract]
    public class Descriptor
    {
        [ProtoMember(1, Name = "single")] public Single Single { get; set; }
        [ProtoMember(2, Name = "multi")] public Multi Multi { get; set; }
    }
}