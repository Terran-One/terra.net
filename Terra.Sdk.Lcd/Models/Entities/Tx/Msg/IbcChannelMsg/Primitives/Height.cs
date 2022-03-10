using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using ProtoBuf;

namespace Terra.Sdk.Lcd.Models.Entities.Tx.Msg.IbcChannelMsg.Primitives
{
    [Serializable]
    [ProtoContract]
    public readonly struct Height : ISerializable
    {
        [ProtoMember(1, Name = "revision_number")] public long RevisionNumber { get; }
        [ProtoMember(2, Name = "revision_height")] public long RevisionHeight { get; }

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
