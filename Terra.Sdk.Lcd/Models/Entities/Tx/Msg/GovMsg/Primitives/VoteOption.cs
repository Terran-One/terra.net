using System.Runtime.Serialization;

namespace Terra.Sdk.Lcd.Models.Entities.Tx.Msg.GovMsg.Primitives
{
    public enum VoteOption
    {
        [EnumMember(Value = "VOTE_OPTION_UNSPECIFIED")]
        Unspecified = 0,
        [EnumMember(Value = "VOTE_OPTION_YES")]
        Yes = 1,
        [EnumMember(Value = "VOTE_OPTION_ABSTAIN")]
        Abstain = 2,
        [EnumMember(Value = "VOTE_OPTION_NO")]
        No = 3,
        [EnumMember(Value = "VOTE_OPTION_NO_WITH_VETO")]
        NoWithVeto = 4,
        [EnumMember(Value = "UNRECOGNIZED")]
        Unrecognized = -1
    }
}
