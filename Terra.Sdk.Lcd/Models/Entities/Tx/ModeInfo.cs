using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Terra.Sdk.Lcd.Models.Entities.Tx
{
    public class ModeInfo
    {
        public SingleMode Single { get; set; }
        public MultiMode Multi { get; set; }

        public class SingleMode
        {
            public SignMode Mode { get; set; }
            public string Signature { get; set; }
        }

        public class MultiMode
        {
            [JsonProperty("bitarray")]
            public CompactBitArray BitArray { get; set; }

            [JsonProperty("modeInfos")]
            public List<ModeInfo> ModeInfos { get; set; }
        }

        public enum SignMode
        {
            [EnumMember(Value = "SIGN_MODE_UNSPECIFIED")]
            Unspecified = 0,
            [EnumMember(Value = "SIGN_MODE_DIRECT")]
            Direct = 1,
            [EnumMember(Value = "SIGN_MODE_TEXTUAL")]
            Textual = 2,
            [EnumMember(Value = "SIGN_MODE_LEGACY_AMINO_JSON")]
            LegacyAminoJson = 127,
            [EnumMember(Value = "UNRECOGNIZED")]
            Unrecognized = -1
        }
    }
}
