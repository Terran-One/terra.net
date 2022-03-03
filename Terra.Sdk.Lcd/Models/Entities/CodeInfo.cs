namespace Terra.Sdk.Lcd.Models.Entities
{
    public class CodeInfo
    {
        private readonly LcdClient _client;

        /// <remarks>
        /// For serialization.
        /// </remarks>
        public CodeInfo()
        {
        }

        internal CodeInfo(LcdClient client)
        {
            _client = client;
        }
    }

    public class ContractInfo
    {
        private readonly LcdClient _client;

        /// <remarks>
        /// For serialization.
        /// </remarks>
        public ContractInfo()
        {
        }

        internal ContractInfo(LcdClient client)
        {
            _client = client;
        }
    }

    public class WasmParams
    {
        private readonly LcdClient _client;

        /// <remarks>
        /// For serialization.
        /// </remarks>
        public WasmParams()
        {
        }

        internal WasmParams(LcdClient client)
        {
            _client = client;
        }
    }
}
