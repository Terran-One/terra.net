namespace Terra.Sdk.Lcd.Models.Entities
{
    public class MarketParams
    {
        private readonly LcdClient _client;

        /// <remarks>
        /// For serialization.
        /// </remarks>
        public MarketParams()
        {
        }

        internal MarketParams(LcdClient client)
        {
            _client = client;
        }
    }
    public class MintingParams
    {
        private readonly LcdClient _client;

        /// <remarks>
        /// For serialization.
        /// </remarks>
        public MintingParams()
        {
        }

        internal MintingParams(LcdClient client)
        {
            _client = client;
        }
    }
}
