using ProtoBuf;

namespace Terra.Sdk.Lcd.Models.Entities.PubKey
{
    [ProtoContract]
    public class ValConsPublicKey : PublicKey
    {
        [ProtoMember(1)]
        public override string Type { get; set; }

        [ProtoMember(2)]
        public override string Key { get; set; }
    }
}
