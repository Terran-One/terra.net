using System.Collections.Generic;
using Terra.Sdk.Lcd.Models.Entities.Tx.Msg.GovMsg.Primitives;
using ProtoBuf;

namespace Terra.Sdk.Lcd.Models.Entities.Tx.Msg.GovMsg
{
    [ProtoContract]
    public class MsgVoteWeighted : Msg
    {
        protected override System.Type Type => typeof(MsgVoteWeighted);

        [ProtoMember(1, Name = "proposal_id")] public long ProposalId { get; set; }
        [ProtoMember(2, Name = "voter")] public string Voter { get; set; }
        [ProtoMember(3, Name = "options")] public List<WeightedVoteOption> Options { get; set; }
    }
}
