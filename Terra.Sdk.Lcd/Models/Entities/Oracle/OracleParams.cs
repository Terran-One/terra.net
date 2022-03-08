using System.Collections.Generic;
using System.Threading.Tasks;
using Terra.Sdk.Lcd.Extensions;

namespace Terra.Sdk.Lcd.Models.Entities.Oracle
{
    public class OracleParams
    {
        private readonly LcdClient _client;

        /// <remarks>
        /// For serialization.
        /// </remarks>
        public OracleParams()
        {
        }

        internal OracleParams(LcdClient client)
        {
            _client = client;
        }

        public string VotePeriod { get; set; }
        public string VoteThreshold { get; set; }
        public string RewardBand { get; set; }
        public string RewardDistributionWindow { get; set; }
        public List<OracleWhitelist> Whitelist { get; set; }
        public string SlashFraction { get; set; }
        public string SlashWindow { get; set; }
        public string MinValidPerWindow { get; set; }

        internal Task<Result<OracleParams>> Get()
        {
            return _client.GetResult(
                "/terra/oracle/v1beta1/params",
                new {Params = new OracleParams()},
                data => new Result<OracleParams> {Value = data.Params});
        }
    }
}