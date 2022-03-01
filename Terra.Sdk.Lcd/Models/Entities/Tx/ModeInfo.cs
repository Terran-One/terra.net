using System.Collections.Generic;
using Newtonsoft.Json;

namespace Terra.Sdk.Lcd.Models.Entities.Tx
{
    public class ModeInfo
    {
        [JsonProperty("single")]
        public SingleMode Single { get; set; }

        [JsonProperty("multi")]
        public MultiMode Multi { get; set; }

        public class SingleMode
        {
            [JsonProperty("mode")]
            public ModeInfo Mode { get; set; }
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