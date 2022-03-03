namespace Terra.Sdk.Lcd.Models.Entities.Ibc
{
    public class IbcTransferParams
    {
        private readonly LcdClient _client;

        /// <remarks>
        /// For serialization.
        /// </remarks>
        public IbcTransferParams()
        {
        }

        internal IbcTransferParams(LcdClient client)
        {
            _client = client;
        }

        public bool SendEnabled { get; set; }
        public bool ReceiveEnabled { get; set; }
    }
}
