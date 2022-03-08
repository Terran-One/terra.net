using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;

namespace Terra.Sdk.Lcd.Models.Entities.Tx
{
    [ProtoContract]
    public class ModeInfo
    {
        [ProtoMember(1)] public SingleMode Single { get; set; }
        [ProtoMember(2)] public MultiMode Multi { get; set; }

        [ProtoContract]
        public class SingleMode
        {
            [ProtoMember(1)] public SignMode Mode { get; set; }
            [ProtoMember(2)] public string Signature { get; set; }
        }

        [ProtoContract]
        public class MultiMode
        {
            [JsonProperty("bitarray")]
            [ProtoMember(1)]
            public CompactBitArray BitArray { get; set; }

            [JsonProperty("modeInfos")]
            [ProtoMember(2)]
            public List<ModeInfo> ModeInfos { get; set; }
        }

        [ProtoContract]
        public enum SignMode
        {
            [EnumMember(Value = "SIGN_MODE_UNSPECIFIED")] [ProtoEnum(Name = "SIGN_MODE_UNSPECIFIED")]
            Unspecified = 0,

            [EnumMember(Value = "SIGN_MODE_DIRECT")] [ProtoEnum(Name = "SIGN_MODE_DIRECT")]
            Direct = 1,

            [EnumMember(Value = "SIGN_MODE_TEXTUAL")] [ProtoEnum(Name = "SIGN_MODE_TEXTUAL")]
            Textual = 2,

            [EnumMember(Value = "SIGN_MODE_LEGACY_AMINO_JSON")] [ProtoEnum(Name = "SIGN_MODE_LEGACY_AMINO_JSON")]
            LegacyAminoJson = 127,

            [EnumMember(Value = "UNRECOGNIZED")] [ProtoEnum(Name = "UNRECOGNIZED")]
            Unrecognized = -1
        }
    }
}