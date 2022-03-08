using Terra.Sdk.Lcd.Models.Entities.Tx.Msg.GovMsg.Primitives;

using ProtoBuf; namespace Terra.Sdk.Lcd.Models.Entities.Tx.Msg.GovMsg
{
    [ProtoContract]public class MsgVote : Msg
    {
        [ProtoMember(1)]public long ProposalId { get; set; }
        [ProtoMember(2)]public string Voter { get; set; }
        [ProtoMember(3)]public VoteOption Option { get; set; }
    }
}
