using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Threading.Tasks;
using ProtoBuf;
using Terra.Sdk.Lcd.Extensions;

namespace Terra.Sdk.Lcd.Models.Entities.Deposit
{
    [Serializable]
    [ProtoContract]
    public readonly struct Tally : ISerializable
    {
        public Tally(int yes, int abstain, int no, int noWithVeto)
        {
            Yes = yes;
            Abstain = abstain;
            No = no;
            NoWithVeto = noWithVeto;
        }

        /// <remarks>
        /// Used for serialization.
        /// </remarks>
        public Tally(SerializationInfo info, StreamingContext text) : this()
        {
            Yes = info.GetInt64("yes");
            Abstain = info.GetInt64("abstain");
            No = info.GetInt64("no");
            NoWithVeto = info.GetInt64("no_with_veto");
        }

        [ProtoMember(1, Name = "yes")] public long Yes { get; }
        [ProtoMember(2, Name = "abstain")] public long Abstain { get; }
        [ProtoMember(3, Name = "no")] public long No { get; }
        [ProtoMember(4, Name = "no_with_veto")] public long NoWithVeto { get; }

        /// <remarks>
        /// Called during serialization.
        /// </remarks>
        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("yes", Yes);
            info.AddValue("abstain", Abstain);
            info.AddValue("no", No);
            info.AddValue("no_with_veto", NoWithVeto);
        }

        internal static Task<Result<Tally>> GetByProposal(LcdClient client, long proposalId)
        {
            return client.GetResult<Tally>($"/cosmos/gov/v1beta1/proposals/{proposalId}/tally");
        }
    }
}
