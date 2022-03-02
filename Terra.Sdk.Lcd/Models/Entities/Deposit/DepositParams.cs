using System.Collections.Generic;
using System.Threading.Tasks;
using Terra.Sdk.Lcd.Extensions;

namespace Terra.Sdk.Lcd.Models.Entities.Deposit
{
    public class DepositParams
    {
        private readonly LcdClient _client;

        /// <remarks>
        /// For serialization.
        /// </remarks>
        public DepositParams()
        {
        }

        public DepositParams(LcdClient client)
        {
            _client = client;
        }

        public List<Coin> MinDeposit { get; set; }
        public string MaxDepositPeriod { get; set; }

        public Task<Result<DepositParams>> Get()
        {
            return _client.GetResult<DepositParams>("/cosmos/gov/v1beta1/params/deposit");
        }
    }
}
