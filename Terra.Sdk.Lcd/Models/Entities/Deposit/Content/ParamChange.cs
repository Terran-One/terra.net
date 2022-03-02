using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Terra.Sdk.Lcd.Models.Entities.Deposit.Content
{
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

        public string Subspace { get; }
        public string Key { get; }
        public string Value { get; }

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