namespace Terra.Sdk.Lcd.Models.Entities
{
    public class DelegateValidator
    {
        private readonly LcdClient _client;

        /// <remarks>
        /// For serialization.
        /// </remarks>
        public DelegateValidator()
        {
        }

        internal DelegateValidator(LcdClient client)
        {
            _client = client;
        }
    }
}
