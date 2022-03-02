using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Terra.Sdk.Lcd.Models.Entities.Deposit
{
    public readonly struct FinalTallyResult : ISerializable
    {
        public FinalTallyResult(int yes, int abstain, int no, int noWithVeto)
        {
            Yes = yes;
            Abstain = abstain;
            No = no;
            NoWithVeto = noWithVeto;
        }

        /// <remarks>
        /// Used for serialization.
        /// </remarks>
        public FinalTallyResult(SerializationInfo info, StreamingContext text) : this()
        {
            Yes = info.GetInt32("yes");
            Abstain = info.GetInt32("abstain");
            No = info.GetInt32("no");
            NoWithVeto = info.GetInt32("no_with_veto");
        }

        public int Yes { get; }
        public int Abstain { get; }
        public int No { get; }
        public int NoWithVeto { get; }

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
    }
}
