using System;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Terra.Sdk.Lcd.Models;
using Terra.Sdk.Lcd.Models.Entities;
using Terra.Sdk.Lcd.Models.Entities.BlockInfo;

namespace Terra.Sdk.Lcd.Api
{
    public class Tendermint
    {
        private readonly LcdClient _client;

        internal Tendermint(LcdClient client)
        {
            _client = client;
        }

        public Task<Result<JObject>> GetNodeInfo()
        {
            throw new NotImplementedException();
        }

        public Task<Result<bool>> GetSyncing()
        {
            throw new NotImplementedException();
        }

        public Task<PaginatedResult<DelegateValidator>> GetValidatorSet(long? height = null)
        {
            throw new NotImplementedException();
        }

        public Task<Result<BlockInfo>> GetBlockInfo(long? height = null) => new BlockInfo(_client).Get(height);
    }
}
