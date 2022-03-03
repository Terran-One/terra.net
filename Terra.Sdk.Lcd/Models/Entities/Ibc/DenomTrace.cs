namespace Terra.Sdk.Lcd.Models.Entities.Ibc
{
    public class DenomTrace
    {
        private readonly LcdClient _client;

        /// <remarks>
        /// For serialization.
        /// </remarks>
        public DenomTrace()
        {
        }

        internal DenomTrace(LcdClient client)
        {
            _client = client;
        }

        public string Path { get; set; }
        public string BaseDenom { get; set; }
    }
}
