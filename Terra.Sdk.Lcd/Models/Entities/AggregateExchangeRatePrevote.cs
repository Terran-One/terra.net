namespace Terra.Sdk.Lcd.Models.Entities
{
    public class AggregateExchangeRatePrevote
    {
        private readonly LcdClient _client;

        /// <remarks>
        /// For serialization.
        /// </remarks>
        public AggregateExchangeRatePrevote()
        {
        }

        internal AggregateExchangeRatePrevote(LcdClient client)
        {
            _client = client;
        }
    }

    public class AggregateExchangeRateVote
    {
        private readonly LcdClient _client;

        /// <remarks>
        /// For serialization.
        /// </remarks>
        public AggregateExchangeRateVote()
        {
        }

        internal AggregateExchangeRateVote(LcdClient client)
        {
            _client = client;
        }
    }

    public class OracleParams
    {
        private readonly LcdClient _client;

        /// <remarks>
        /// For serialization.
        /// </remarks>
        public OracleParams()
        {
        }

        internal OracleParams(LcdClient client)
        {
            _client = client;
        }
    }
}
