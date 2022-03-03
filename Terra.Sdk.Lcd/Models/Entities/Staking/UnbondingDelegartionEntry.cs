using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Terra.Sdk.Lcd.Models.Entities.Staking
{
    public class UnbondingDelegartionEntry : ISerializable
    {
        public int InitialBalance { get; }
        public int Balance { get; }
        public long CreationHeight { get; }
        public DateTime CompletionTime { get; }

        /// <remarks>
        /// Used for serialization.
        /// </remarks>
        public UnbondingDelegartionEntry(SerializationInfo info, StreamingContext text)
        {
            InitialBalance = info.GetInt32("initial_balance");
            Balance = info.GetInt32("balance");
            CreationHeight = info.GetInt64("creation_height");
            CompletionTime = info.GetDateTime("completion_time");
        }

        public UnbondingDelegartionEntry(int initialBalance, int balance, long creationHeight, DateTime completionTime)
        {
            InitialBalance = initialBalance;
            Balance = balance;
            CreationHeight = creationHeight;
            CompletionTime = completionTime;
        }

        /// <remarks>
        /// Called during serialization.
        /// </remarks>
        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("initial_balance", InitialBalance);
            info.AddValue("balance", Balance);
            info.AddValue("creation_height", CreationHeight);
            info.AddValue("completion_time", CompletionTime);
        }
    }
}