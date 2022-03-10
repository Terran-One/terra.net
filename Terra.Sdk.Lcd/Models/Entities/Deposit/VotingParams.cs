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

        internal VotingParams(LcdClient client)
        {
            _client = client;
        }

        public string VotingPeriod { get; set; }

        internal Task<Result<VotingParams>> Get()
        {
            return _client.GetResult(
                "/cosmos/gov/v1beta1/params/voting",
                new {VotingParams = new VotingParams()},
                data => new Result<VotingParams> {Value = data.VotingParams});
        }
    }
}
