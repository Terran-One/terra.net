using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Terra.Sdk.Lcd.Extensions;

namespace Terra.Sdk.Lcd.Models.Entities.Tx
{
    public class TxInfo
    {
        private readonly LcdClient _client;

        /// <remarks>
        /// For serialization.
        /// </remarks>
        public TxInfo()
        {
        }

        internal TxInfo(LcdClient lcdClient)
        {
            _client = lcdClient;
        }

        public string Height { get; set; }
        [JsonProperty("txhash")]
        public string TxHash { get; set; }
        [JsonProperty("codespace")]
        public string CodeSpace { get; set; }
        public string Code { get; set; }
        public string Data { get; set; }
        public string RawLog { get; set; }
        public List<TxLog> Logs { get; set; }
        public string Info { get; set; }
        public string GasWanted { get; set; }
        public string GasUsed { get; set; }
        public Tx Tx { get; set; }
        public DateTime Timestamp { get; set; }

        public Task<Result<TxInfo>> Get(string txHash)
        {
            return _client.GetResult(
                $"/cosmos/tx/v1beta1/txs/{txHash}",
                new {TxResponse = new TxInfo()},
                data => new Result<TxInfo> {Value = data.TxResponse});
        }

        public async Task<Result<List<TxInfo>>> GetByHeight(long height)
        {
            var blockInfo = await new BlockInfo.BlockInfo(_client).Get(height);
            if (blockInfo.Error != null)
                return new Result<List<TxInfo>> { Error = blockInfo.Error };

            var txs = blockInfo.Value.Block?.Data?.Txs;
            if (txs == null)
                return new Result<List<TxInfo>> { Value = new List<TxInfo>() };

            var txInfos = new List<TxInfo>();
            Error firstError = null;

            var txHashes = txs.Select(tx => tx.GetSha256Hash());
            foreach (var txHash in txHashes)
            {
                var result = await Get(txHash);
                if (result.Value != null)
                {
                    txInfos.Add(result.Value);
                }
                else if (result.Error != null)
                {
                    firstError = result.Error;
                }
            }

            if (!txInfos.Any() && firstError != null)
                return new Result<List<TxInfo>> { Error = firstError };

            return new Result<List<TxInfo>> { Value = txInfos };
        }
    }
}
