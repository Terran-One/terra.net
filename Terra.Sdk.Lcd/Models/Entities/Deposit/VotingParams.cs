using System.Threading.Tasks;
using Terra.Sdk.Lcd.Extensions;

namespace Terra.Sdk.Lcd.Models.Entities.Deposit
{
    public class VotingParams
    {
        private readonly LcdClient _client;

        /// <remarks>
        /// For serialization.
        /// </remarks>
        public VotingParams()
        {
        }

        public VotingParams(LcdClient client)
        {
            _client = client;
        }

        public long VotingPeriod { get; set; }

        public Task<Result<VotingParams>> Get()
        {
            return _client.GetResult<VotingParams>("/cosmos/gov/v1beta1/params/voting");
        }
    }
}
