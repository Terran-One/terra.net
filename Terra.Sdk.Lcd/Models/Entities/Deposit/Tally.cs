using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Threading.Tasks;
using ProtoBuf;
using Terra.Sdk.Lcd.Extensions;

namespace Terra.Sdk.Lcd.Models.Entities.Deposit
{
    [ProtoContract]public readonly struct Tally : ISerializable
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
            Yes = info.GetInt32("yes");
            Abstain = info.GetInt32("abstain");
            No = info.GetInt32("no");
            NoWithVeto = info.GetInt32("no_with_veto");
        }

        [ProtoMember(1)]public int Yes { get; }
        [ProtoMember(2)]public int Abstain { get; }
        [ProtoMember(3)]public int No { get; }
        [ProtoMember(4)]public int NoWithVeto { get; }

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
