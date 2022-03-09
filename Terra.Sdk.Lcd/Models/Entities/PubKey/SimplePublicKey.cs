using ProtoBuf;

namespace Terra.Sdk.Lcd.Models.Entities.PubKey
{
    [ProtoContract]
    public class SimplePublicKey : PublicKey
    {
        [ProtoMember(1, Name = "key")] public override string Key { get; set; }
    }
}
