using System.Collections.Generic;

namespace Terra.Sdk.Lcd.Models.Entities.Tx.Msg.GovMsg
{
    public class MsgDeposit : Msg
    {
        public string ProposalId { get; set;  }
        public string Depositor { get; set; }
        public List<Coin> Amount { get; set; }
    }
}
