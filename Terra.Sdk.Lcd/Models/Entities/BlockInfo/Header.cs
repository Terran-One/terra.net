namespace Terra.Sdk.Lcd.Models.Entities.BlockInfo
{
    public class Header
    {
        public Tx.Msg.IbcConnectionMsg.Primitives.Version Version { get; set; }
        public string ChainId { get; set; }
        public string Height { get; set; }
        public string Time { get; set; }
        public BlockId LastBlockId { get; set; }
        public string LastCommitHash { get; set; }
        public string DataHash { get; set; }
        public string ValidatorsHash { get; set; }
        public string NextValidatorsHash { get; set; }
        public string ConsensusHash { get; set; }
        public string AppHash { get; set; }
        public string LastResultsHash { get; set; }
        public string EvidenceHash { get; set; }
        public string ProposerAddress { get; set; }
    }
}
