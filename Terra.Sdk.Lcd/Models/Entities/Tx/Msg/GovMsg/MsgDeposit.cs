using System.Collections.Generic;
using ProtoBuf;

namespace Terra.Sdk.Lcd.Models.Entities.Tx.Msg.GovMsg
{
    [ProtoContract]
    public class MsgDeposit : Msg
    {
        [ProtoMember(1, Name = "proposal_id")] public string ProposalId { get; set; }
        [ProtoMember(2, Name = "depositor")] public string Depositor { get; set; }
        [ProtoMember(3, Name = "amount")] public List<Coin> Amount { get; set; }
    }
}
