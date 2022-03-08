using System.Collections.Generic;
using System.Threading.Tasks;
using Terra.Sdk.Lcd.Extensions;
using Terra.Sdk.Lcd.Models.Entities.PubKey;
using Terra.Sdk.Lcd.Models.Entities.Tx.Msg.StakingMsg.Primitives;

namespace Terra.Sdk.Lcd.Models.Entities.Staking
{
    public class Validator
    {
        private readonly LcdClient _client;

        /// <remarks>
        /// For serialization.
        /// </remarks>
        public Validator()
        {
        }

        internal Validator(LcdClient client)
        {
            _client = client;
        }

        public string OperatorAddress { get; set; }
        public ValConsPublicKey ConsensusPubkey { get; set; }
        public bool Jailed { get; set; }
        public BondStatus Status { get; set; }
        public string Tokens { get; set; }
        public string DelegatorShares { get; set; }
        public Description Description { get; set; }
        public string UnbondingHeight { get; set; }
        public string UnbondingTime { get; set; }
        public Commission Commission { get; set; }
        public string MinSelfDelegation { get; set; }

        internal Task<PaginatedResult<Validator>> GetAllBonded(string delegator, string paginationKey = null, int? pageNumber = null, bool? getTotalCount = null, bool? isDescending = null)
        {
            return _client.GetPaginatedResult(
                $"/cosmos/staking/v1beta1/delegators/{delegator}/validators",
                new {Validators = new List<Validator>(), Pagination = new Pagination()},
                data => data.Pagination.BuildResult(data.Validators, pageNumber),
                paginationKey, pageNumber, getTotalCount, isDescending);
        }

        internal Task<Result<Validator>> Get(string validator)
        {
            return _client.GetResult(
                $"/cosmos/staking/v1beta1/validators/{validator}",
                new {Validator = new Validator()},
                data => new Result<Validator> {Value = data.Validator});
        }

        internal Task<PaginatedResult<Validator>> GetAll(string paginationKey = null, int? pageNumber = null, bool? getTotalCount = null, bool? isDescending = null)
        {
            return _client.GetPaginatedResult(
                "/cosmos/staking/v1beta1/validators",
                new {Validators = new List<Validator>(), Pagination = new Pagination()},
                data => data.Pagination.BuildResult(data.Validators, pageNumber),
                paginationKey, pageNumber, getTotalCount, isDescending);
        }
    }
}