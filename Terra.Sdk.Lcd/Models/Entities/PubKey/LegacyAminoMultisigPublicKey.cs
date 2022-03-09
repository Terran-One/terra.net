using System.Collections.Generic;
using System.Linq;
using ProtoBuf;

namespace Terra.Sdk.Lcd.Models.Entities.PubKey
{
    [ProtoContract]
    public class LegacyAminoMultisigPublicKey : PublicKey
    {
        [ProtoMember(1, Name = "type")] public override string Type { get; set; }

        [ProtoMember(2, Name = "key")]
        public override string Key
        {
            get => PublicKeys?.FirstOrDefault()?.Key;
            set { }
        }

        [ProtoMember(3, Name = "threshold")] public string Threshold { get; set; }

        [ProtoMember(4, Name = "public_keys")] public List<SimplePublicKey> PublicKeys { get; set; }
    }
}
