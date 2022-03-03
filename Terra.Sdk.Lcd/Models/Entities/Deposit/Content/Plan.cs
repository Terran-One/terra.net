using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Terra.Sdk.Lcd.Models.Entities.Deposit.Content
{
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

        public string Name { get; }
        public string Time { get; }
        public string Height { get; }
        public string Info { get; }
        public string UpgradedClientState { get; }

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