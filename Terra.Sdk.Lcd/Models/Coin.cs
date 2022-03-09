using System;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Newtonsoft.Json;
using ProtoBuf;

namespace Terra.Sdk.Lcd.Models
{
    [ProtoContract]
    [Serializable]
    public readonly struct Coin : ISerializable
    {
        [ProtoMember(1, Name = "denom")]
        public string Denom { get; }

        public decimal Amount { get; }

        /// <remarks>
        /// For protobuf serialization.
        /// </remarks>
        [JsonIgnore]
        [ProtoMember(2, Name = "amount")]
        public string ProtoAmount { get; }

        public Coin(string denom, decimal amount) : this()
        {
            Denom = denom;
            Amount = amount;
            ProtoAmount = amount.ToString(CultureInfo.InvariantCulture);
        }

        /// <remarks>
        /// Used for serialization.
        /// </remarks>
        public Coin(SerializationInfo info, StreamingContext text) : this()
        {
            Denom = info.GetString("denom");
            Amount = info.GetDecimal("amount");
            ProtoAmount = info.GetString("amount");
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

        public override string ToString()
        {
            var roundedAmount = Math.Round(Amount);
            return Amount == roundedAmount ? $"{roundedAmount}.0{Denom}" : $"{roundedAmount}${Denom}";
        }
    }
}
