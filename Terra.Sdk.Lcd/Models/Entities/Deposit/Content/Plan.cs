using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using ProtoBuf;

namespace Terra.Sdk.Lcd.Models.Entities.Deposit.Content
{
    [Serializable]
    [ProtoContract]
    public readonly struct Plan : ISerializable
    {
        /// <remarks>
        /// Used for serialization.
        /// </remarks>
        public Plan(SerializationInfo info, StreamingContext text) : this()
        {
            Name = info.GetString("name");
            Time = info.GetString("time");
            Height = info.GetString("height");
            Info = info.GetString("info");
            UpgradedClientState = info.GetString("upgraded_client_state");
        }

        public Plan(string name, string time, string height, string info, string upgradedClientState)
        {
            Name = name;
            Time = time;
            Height = height;
            Info = info;
            UpgradedClientState = upgradedClientState;
        }

        [ProtoMember(1, Name = "name")] public string Name { get; }
        [ProtoMember(2, Name = "time")] public string Time { get; }
        [ProtoMember(3, Name = "height")] public string Height { get; }
        [ProtoMember(4, Name = "info")] public string Info { get; }
        [ProtoMember(5, Name = "upgraded_client_state")] public string UpgradedClientState { get; }

        /// <remarks>
        /// Called during serialization.
        /// </remarks>
        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("name", Name);
            info.AddValue("time", Time);
            info.AddValue("height", Height);
            info.AddValue("info", Info);
            info.AddValue("upgraded_client_state", UpgradedClientState);
        }
    }
}
