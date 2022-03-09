using System.Collections.Generic;
using Newtonsoft.Json;
using ProtoBuf;
using Terra.Sdk.Lcd.Models.Entities.Tx;

namespace Terra.Sdk.Lcd.Models.Signing
{
    [ProtoContract]
    public class Multi
    {
        [JsonProperty("bitarray")]
        [ProtoMember(1, Name = "bitarray")]
        public CompactBitArray BitArray { get; set; }

        [JsonProperty("modeInfos")]
        [ProtoMember(2, Name = "signatures")]
        public List<Descriptor> Signatures { get; set; }
    }
}