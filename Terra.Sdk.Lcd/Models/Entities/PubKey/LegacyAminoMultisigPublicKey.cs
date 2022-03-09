using System;
using System.Collections.Generic;
using System.Linq;
using ProtoBuf;

namespace Terra.Sdk.Lcd.Models.Entities.PubKey
{
    [ProtoContract]
    public class LegacyAminoMultisigPublicKey : PublicKey
    {
        protected override Type Type => typeof(LegacyAminoMultisigPublicKey);

        [ProtoMember(1, Name = "key")]
        public override string Key
        {
            get => PublicKeys?.FirstOrDefault()?.Key;
            set { }
        }

        [ProtoMember(2, Name = "threshold")]
        public string Threshold { get; set; }

        [ProtoMember(3, Name = "public_keys")]
        public List<SimplePublicKey> PublicKeys { get; set; }
    }
}
