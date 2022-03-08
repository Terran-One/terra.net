using System.Collections.Generic;
using ProtoBuf;

namespace Terra.Sdk.Lcd.Models.Entities.Tx.Msg.GovMsg
{
    [ProtoContract]
    public class MsgDeposit : Msg
    {
        [ProtoMember(1)] public string ProposalId { get; set; }
        [ProtoMember(2)] public string Depositor { get; set; }
        [ProtoMember(3)] public List<Coin> Amount { get; set; }
    }
}