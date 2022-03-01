using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Terra.Sdk.Lcd.Models.Entities.Tx.Msg.GovMsg
{
    public class MsgVoteWeighted : Msg
    {
        public long ProposalId { get; set; }
        public string Voter { get; set; }
        public List<WeightedVoteOption> Options { get; set; }

        public readonly struct WeightedVoteOption : ISerializable
        {
            public MsgVote.VoteOption Option { get; }
            public decimal Weight { get; }

            public WeightedVoteOption(MsgVote.VoteOption option, decimal weight) : this()
            {
                Option = option;
                Weight = weight;
            }

            /// <remarks>
            /// Used for serialization.
            /// </remarks>
            public WeightedVoteOption(SerializationInfo info, StreamingContext text) : this()
            {
                Option = (MsgVote.VoteOption)info.GetInt32("option");
                Weight = info.GetDecimal("weight");
            }

            /// <remarks>
            /// Called during serialization.
            /// </remarks>
            [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
            public void GetObjectData(SerializationInfo info, StreamingContext context)
            {
                info.AddValue("option", (int)Option);
                info.AddValue("weight", Weight);
            }
        }
    }
}
