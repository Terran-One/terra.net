using Newtonsoft.Json;

namespace Terra.Sdk.Lcd.Models.Entities.Tx
{
    public class CompactBitArray
    {
        [JsonProperty("extra_bits_stored")]
        public long ExtraBitsStored { get; set; }

        [JsonProperty("elems")]
        public string Elems { get; set; }
    }
}