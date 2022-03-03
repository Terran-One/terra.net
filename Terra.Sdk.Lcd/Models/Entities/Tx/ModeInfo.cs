using System.Collections.Generic;
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
            Unspecified = 0,
            Direct = 1,
            Textual = 2,
            LegacyAminoJson = 127,
            Unrecognized = -1
        }
    }
}
