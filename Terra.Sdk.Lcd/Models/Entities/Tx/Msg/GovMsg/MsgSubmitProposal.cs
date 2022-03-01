using System.Collections.Generic;

namespace Terra.Sdk.Lcd.Models.Entities.Tx.Msg.GovMsg
{
    public class MsgSubmitProposal : Msg
    {
        public Proposal Content { get; set; }
        public List<Coin> InitialDeposit { get; set; }
        public string Proposer { get; set; }
    }
}
