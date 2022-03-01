using System.Collections.Generic;
using Terra.Sdk.Lcd.Models.Entities.Tx.Msg.GovMsg.Primitives;

namespace Terra.Sdk.Lcd.Models.Entities.Tx.Msg.GovMsg
{
    public class MsgVoteWeighted : Msg
    {
        public long ProposalId { get; set; }
        public string Voter { get; set; }
        public List<WeightedVoteOption> Options { get; set; }
    }
}
