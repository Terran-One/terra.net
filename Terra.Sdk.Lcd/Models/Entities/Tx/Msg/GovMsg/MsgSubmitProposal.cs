using System.Collections.Generic;
using Terra.Sdk.Lcd.Models.Entities.Deposit;

using ProtoBuf; namespace Terra.Sdk.Lcd.Models.Entities.Tx.Msg.GovMsg
{
    [ProtoContract]public class MsgSubmitProposal : Msg
    {
        [ProtoMember(1)]public Proposal Content { get; set; }
        [ProtoMember(2)]public List<Coin> InitialDeposit { get; set; }
        [ProtoMember(3)]public string Proposer { get; set; }
    }
}
