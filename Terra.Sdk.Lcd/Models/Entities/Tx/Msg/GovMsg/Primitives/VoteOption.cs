using System.Runtime.Serialization;
using ProtoBuf;

namespace Terra.Sdk.Lcd.Models.Entities.Tx.Msg.GovMsg.Primitives
{
    [ProtoContract]
    public enum VoteOption
    {
        [EnumMember(Value = "VOTE_OPTION_UNSPECIFIED")] [ProtoEnum(Name = "VOTE_OPTION_UNSPECIFIED")]
        Unspecified = 0,

        [EnumMember(Value = "VOTE_OPTION_YES")] [ProtoEnum(Name = "VOTE_OPTION_YES")]
        Yes = 1,

        [EnumMember(Value = "VOTE_OPTION_ABSTAIN")] [ProtoEnum(Name = "VOTE_OPTION_ABSTAIN")]
        Abstain = 2,

        [EnumMember(Value = "VOTE_OPTION_NO")] [ProtoEnum(Name = "VOTE_OPTION_NO")]
        No = 3,

        [EnumMember(Value = "VOTE_OPTION_NO_WITH_VETO")] [ProtoEnum(Name = "VOTE_OPTION_NO_WITH_VETO")]
        NoWithVeto = 4,

        [EnumMember(Value = "UNRECOGNIZED")] [ProtoEnum(Name = "UNRECOGNIZED")]
        Unrecognized = -1
    }
}