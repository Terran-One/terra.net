using System.Runtime.Serialization;
using System.Security.Permissions;
using ProtoBuf;

namespace Terra.Sdk.Lcd.Models.Entities.Deposit.Content
{
    [ProtoContract]
    public readonly struct ParamChange : ISerializable
    {
        /// <remarks>
        /// Used for serialization.
        /// </remarks>
        public ParamChange(SerializationInfo info, StreamingContext text) : this()
        {
            Subspace = info.GetString("subspace");
            Key = info.GetString("key");
            Value = info.GetString("value");
        }

        public ParamChange(string subspace, string key, string value) : this()
        {
            Subspace = subspace;
            Key = key;
            Value = value;
        }

        [ProtoMember(1, Name = "subspace")] public string Subspace { get; }
        [ProtoMember(2, Name = "key")] public string Key { get; }
        [ProtoMember(3, Name = "value")] public string Value { get; }

        /// <remarks>
        /// Called during serialization.
        /// </remarks>
        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("subspace", Subspace);
            info.AddValue("key", Key);
            info.AddValue("value", Value);
        }
    }
}
