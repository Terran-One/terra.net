using System;
using ProtoBuf;

namespace Terra.Sdk.Lcd.Models.Entities.PubKey
{
    [ProtoContract]
    public class ValConsPublicKey : PublicKey
    {
        protected override Type Type => typeof(ValConsPublicKey);

        [ProtoMember(1, Name = "key")] public override string Key { get; set; }
    }
}
