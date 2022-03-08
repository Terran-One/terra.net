using System.Collections.Generic;
using System.Threading.Tasks;
using Terra.Sdk.Lcd.Extensions;
using Terra.Sdk.Lcd.Models.Entities.PubKey;

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

        public string Address { get; set; }
        public ValConsPublicKey PubKey { get; set; }
        public string ProposerPriority { get; set; }
        public string VotingPower { get; set; }

        public Task<PaginatedResult<DelegateValidator>> Get(long? height = null,
            string paginationKey = null, int? pageNumber = null, bool? getTotalCount = null, bool? isDescending = null)
        {
            var url = height.HasValue
                ? $"/cosmos/base/tendermint/v1beta1/validatorsets/{height}"
                : "/cosmos/base/tendermint/v1beta1/validatorsets/latest";

            return _client.GetPaginatedResult(
                url,
                new {Validators = new List<DelegateValidator>(), Pagination = new Pagination()},
                data => data.Pagination.BuildResult(data.Validators, pageNumber),
                paginationKey, pageNumber, getTotalCount, isDescending);
        }
    }
}