namespace Terra.Sdk.Lcd.Models.Entities.BlockInfo
{
    public class Block
    {
        public Header Header { get; set; }
        public BlockData Data { get; set; }
        public BlockEvidence Evidence { get; set; }
        public LastCommit LastCommit { get; set; }
    }
}