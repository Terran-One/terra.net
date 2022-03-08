using System.Collections.Generic;
using System.Linq;
using ProtoBuf;

namespace Terra.Sdk.Lcd.Models.Entities.PubKey
{
    [ProtoContract]
    public class LegacyAminoMultisigPublicKey : PublicKey
    {
        [ProtoMember(1)] public override string Type { get; set; }

        [ProtoMember(2)]
        public override string Key
        {
            get => PublicKeys?.FirstOrDefault()?.Key;
            set { }
        }

        [ProtoMember(3)] public string Threshold { get; set; }

        [ProtoMember(4)] public List<SimplePublicKey> PublicKeys { get; set; }
    }
}