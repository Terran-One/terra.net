using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Terra.Sdk.Lcd.Models.Entities.Tx.Msg.StakingMsg.Primitives
{
    public readonly struct CommissionRates : ISerializable
    {
        public decimal Rate { get; }
        public decimal MaxRate { get; }
        public decimal MaxChangeRate { get; }

        public CommissionRates(decimal rate, decimal maxRate, decimal maxChangeRate) : this()
        {
            Rate = rate;
            MaxRate = maxRate;
            MaxChangeRate = maxChangeRate;
        }

        /// <remarks>
        /// Used for serialization.
        /// </remarks>
        public CommissionRates(SerializationInfo info, StreamingContext text) : this()
        {

            Rate = info.GetDecimal("rate");
            MaxRate = info.GetDecimal("max_rate");
            MaxChangeRate = info.GetDecimal("max_change_rate");
        }

        /// <remarks>
        /// Called during serialization.
        /// </remarks>
        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("rate", Rate);
            info.AddValue("max_rate", MaxRate);
            info.AddValue("max_change_rate", MaxChangeRate);
        }
    }
}
