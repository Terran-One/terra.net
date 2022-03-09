using ProtoBuf;

namespace Terra.Sdk.Lcd.Models.Entities.PubKey
{
    [ProtoContract]
    public class ValConsPublicKey : PublicKey
    {
        [ProtoMember(1, Name = "type")] public override string Type { get; set; }
        [ProtoMember(2, Name = "key")] public override string Key { get; set; }
    }
}
