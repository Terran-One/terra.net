using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Terra.Sdk.Lcd.Models.Entities.Tx.Msg.IbcChannelMsg.Primitives
{
    public readonly struct Height : ISerializable
    {
        public long RevisionNumber { get; }
        public long RevisionHeight { get; }

        public Height(long revisionNumber, long revisionHeight) : this()
        {
            RevisionNumber = revisionNumber;
            RevisionHeight = revisionHeight;
        }

        /// <remarks>
        /// Used for serialization.
        /// </remarks>
        public Height(SerializationInfo info, StreamingContext text) : this()
        {
            RevisionNumber = info.GetInt64("revision_number");
            RevisionHeight = info.GetInt64("revision_height");
        }

        /// <remarks>
        /// Called during serialization.
        /// </remarks>
        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("revision_number", RevisionNumber);
            info.AddValue("revision_height", RevisionHeight);
        }
    }
}
