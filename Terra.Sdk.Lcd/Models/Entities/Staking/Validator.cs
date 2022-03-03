using System;
using System.Threading.Tasks;
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

        internal Task<PaginatedResult<Validator>> GetAllBonded(string delegator, string paginationKey, int? pageNumber, bool? getTotalCount, bool? isDescending)
        {
            throw new NotImplementedException();
        }

        internal Task<Result<Validator>> Get(string address)
        {
            throw new NotImplementedException();
        }

        internal Task<PaginatedResult<Validator>> GetAll(string paginationKey, int? pageNumber, bool? getTotalCount, bool? isDescending)
        {
            throw new NotImplementedException();
        }
    }
}