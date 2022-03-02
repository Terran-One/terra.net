using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Terra.Sdk.Lcd.Models
{
    [Serializable]
    public readonly struct Coin : ISerializable
    {
        public string Denom { get; }
        public decimal Amount { get; }

        public Coin(string denom, decimal amount) : this()
        {
            Denom = denom;
            Amount = amount;
        }

        /// <remarks>
        /// Used for serialization.
        /// </remarks>
        public Coin(SerializationInfo info, StreamingContext text) : this()
        {
            Denom = info.GetString("denom");
            Amount = info.GetDecimal("amount");
        }

        /// <remarks>
        /// Called during serialization.
        /// </remarks>
        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("denom", Denom);
            info.AddValue("amount", Amount);
        }

        public Coin Multiply(decimal other) => new Coin(Denom, Amount * other);
        public Coin ToIntCeilCoin() => new Coin(Denom, Math.Ceiling(Amount));
    }
}
