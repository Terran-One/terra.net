using System.Threading.Tasks;
using Terra.Sdk.Lcd.Extensions;

namespace Terra.Sdk.Lcd.Models.Entities.Oracle
{
    public class AggregateExchangeRatePrevote
    {
        private readonly LcdClient _client;

        /// <remarks>
        /// For serialization.
        /// </remarks>
        public AggregateExchangeRatePrevote()
        {
        }

        internal AggregateExchangeRatePrevote(LcdClient client)
        {
            _client = client;
        }

        public string Hash { get; set; }
        public string Voter { get; set; }
        public string SubmitBlock { get; set; }

        internal Task<Result<AggregateExchangeRatePrevote>> Get(string validator)
        {
            return _client.GetResult(
                $"/terra/oracle/v1beta1/validators/{validator}/aggregate_prevote",
                new {AggregatePrevote = new AggregateExchangeRatePrevote()},
                data => new Result<AggregateExchangeRatePrevote> {Value = data.AggregatePrevote});
        }
    }
}