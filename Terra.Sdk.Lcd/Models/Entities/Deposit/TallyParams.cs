using System.Threading.Tasks;
using Terra.Sdk.Lcd.Extensions;

namespace Terra.Sdk.Lcd.Models.Entities.Deposit
{
    public class TallyParams
    {
        private readonly LcdClient _client;

        /// <remarks>
        /// For serialization.
        /// </remarks>
        public TallyParams()
        {
        }

        internal TallyParams(LcdClient client)
        {
            _client = client;
        }

        /// <summary>
        /// Ratio of total staked tokens that need to have participated in the vote.
        /// </summary>
        public decimal Quorum { get; set; }

        /// <summary>
        /// Ratio of participating tokens that have voted in favor of the proposal.
        /// </summary>
        public decimal Threshold { get; set; }

        /// <summary>
        /// Ratio of participating votes with `NoWithVeto` (after excluding `Abstain` votes) to veto the proposal.
        /// </summary>
        public decimal VetoThreshold { get; set; }

        public Task<Result<TallyParams>> Get()
        {
            return _client.GetResult<TallyParams>("/cosmos/gov/v1beta1/params/tallying");
        }
    }
}
