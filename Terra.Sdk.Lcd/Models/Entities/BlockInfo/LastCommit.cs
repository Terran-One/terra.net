using System.Collections.Generic;

namespace Terra.Sdk.Lcd.Models.Entities.BlockInfo
{
    public class LastCommit
    {
        public string Height { get; set; }
        public long Round { get; set; }
        public BlockId BlockId { get; set; }
        public List<BlockSignature> Signatures { get; set; }
    }
}