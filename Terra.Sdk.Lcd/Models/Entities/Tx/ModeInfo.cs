using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;

namespace Terra.Sdk.Lcd.Models.Entities.Tx
{
    [ProtoContract]
    public class ModeInfo
    {
        [ProtoMember(1, Name = "single")] public Single Single { get; set; }
        [ProtoMember(2, Name = "multi")] public Multi Multi { get; set; }
    }

    [ProtoContract]
    public class Single
    {
        [ProtoMember(1, Name = "mode")] public SignMode Mode { get; set; }
        [ProtoMember(2, Name = "signature")] public string Signature { get; set; }
    }

    [ProtoContract]
    public class Multi
    {
        [JsonProperty("bitarray")]
        [ProtoMember(1, Name = "bitarray")]
        public CompactBitArray BitArray { get; set; }

        [JsonProperty("modeInfos")]
        [ProtoMember(2, Name = "mode_infos")]
        public List<ModeInfo> ModeInfos { get; set; }
    }

    [ProtoContract]
    public enum SignMode
    {
        [EnumMember(Value = "SIGN_MODE_UNSPECIFIED")]
        [ProtoEnum(Name = "SIGN_MODE_UNSPECIFIED")]
        Unspecified = 0,

        [EnumMember(Value = "SIGN_MODE_DIRECT")]
        [ProtoEnum(Name = "SIGN_MODE_DIRECT")]
        Direct = 1,

        [EnumMember(Value = "SIGN_MODE_TEXTUAL")]
        [ProtoEnum(Name = "SIGN_MODE_TEXTUAL")]
        Textual = 2,

        [EnumMember(Value = "SIGN_MODE_LEGACY_AMINO_JSON")]
        [ProtoEnum(Name = "SIGN_MODE_LEGACY_AMINO_JSON")]
        LegacyAminoJson = 127,

        [EnumMember(Value = "UNRECOGNIZED")]
        Unrecognized = -1
    }
}
