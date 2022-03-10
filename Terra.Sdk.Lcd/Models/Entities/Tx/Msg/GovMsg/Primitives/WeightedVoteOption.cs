using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Terra.Sdk.Lcd.Models.Entities.Tx.Msg.GovMsg.Primitives
{
    [Serializable]
    public readonly struct WeightedVoteOption : ISerializable
    {
        public VoteOption Option { get; }
        public decimal Weight { get; }

        public WeightedVoteOption(VoteOption option, decimal weight) : this()
        {
            Option = option;
            Weight = weight;
        }

        /// <remarks>
        /// Used for serialization.
        /// </remarks>
        public WeightedVoteOption(SerializationInfo info, StreamingContext text) : this()
        {
            Option = (VoteOption) info.GetInt32("option");
            Weight = info.GetDecimal("weight");
        }

        /// <remarks>
        /// Called during serialization.
        /// </remarks>
        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("option", (int) Option);
            info.AddValue("weight", Weight);
        }
    }
}
