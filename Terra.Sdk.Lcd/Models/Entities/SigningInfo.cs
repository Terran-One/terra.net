namespace Terra.Sdk.Lcd.Models.Entities
{
    public class SigningInfo
    {
        private readonly LcdClient _client;

        /// <remarks>
        /// For serialization.
        /// </remarks>
        public SigningInfo()
        {
        }

        internal SigningInfo(LcdClient client)
        {
            _client = client;
        }
    }

    public class SlashingParams
    {
        private readonly LcdClient _client;

        /// <remarks>
        /// For serialization.
        /// </remarks>
        public SlashingParams()
        {
        }

        internal SlashingParams(LcdClient client)
        {
            _client = client;
        }
    }
}
