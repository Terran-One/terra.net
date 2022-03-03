using System.Collections.Generic;
using System.Threading.Tasks;
using Terra.Sdk.Lcd.Extensions;

namespace Terra.Sdk.Lcd.Models.Entities.Oracle
{
    public class AggregateExchangeRateVote
    {
        private readonly LcdClient _client;

        /// <remarks>
        /// For serialization.
        /// </remarks>
        public AggregateExchangeRateVote()
        {
        }

        internal AggregateExchangeRateVote(LcdClient client)
        {
            _client = client;
        }

        public List<ExchangeRateTuple> ExchangeRateTuples { get; set; }
        public string Voter { get; set; }

        internal Task<Result<AggregateExchangeRateVote>> Get(string validator)
        {
            return _client.GetResult(
                $"/terra/oracle/v1beta1/validators/{validator}/aggregate_vote",
                new {AggregateVote = new AggregateExchangeRateVote()},
                data => new Result<AggregateExchangeRateVote> {Value = data.AggregateVote});
        }
    }
}
