using System.Collections.Generic;
using Terra.Sdk.Lcd.Models.Entities.Deposit;
using ProtoBuf;

namespace Terra.Sdk.Lcd.Models.Entities.Tx.Msg.GovMsg
{
    [ProtoContract]
    public class MsgSubmitProposal : Msg
    {
        protected override System.Type Type => typeof(MsgSubmitProposal);

        [ProtoMember(1, Name = "content")] public Proposal Content { get; set; }
        [ProtoMember(2, Name = "initial_deposit")] public List<Coin> InitialDeposit { get; set; }
        [ProtoMember(3, Name = "proposer")] public string Proposer { get; set; }
    }
}
