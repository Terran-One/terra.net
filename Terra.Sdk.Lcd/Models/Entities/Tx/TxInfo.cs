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
        private readonly LcdClient _lcdClient;

        /// <remarks>
        /// For serialization.
        /// </remarks>
        public TxInfo()
        {
        }

        internal TxInfo(LcdClient lcdClient)
        {
            _lcdClient = lcdClient;
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

        public async Task<Result<TxInfo>> Get(string txHash)
        {
            var response = await _lcdClient.HttpClient.GetAsync($"/cosmos/tx/v1beta1/txs/{txHash}");
            if (!response.IsSuccessStatusCode)
                return new Result<TxInfo> {  Error = $"Fetch failed: {response.ReasonPhrase}" };

            var json = JsonConvert.DeserializeAnonymousType(
                await response.Content.ReadAsStringAsync(),
                new { TxResponse = new TxInfo() },
                _lcdClient.JsonSerializerSettings);
            return new Result<TxInfo> { Value = json.TxResponse };
        }

        public async Task<Result<List<TxInfo>>> GetByHeight(long height)
        {
            var blockInfo = await new BlockInfo.BlockInfo(_lcdClient).Get(height);
            if (blockInfo.Error != null)
                return new Result<List<TxInfo>> { Error = blockInfo.Error };

            var txs = blockInfo.Value.Block?.Data?.Txs;
            if (txs == null)
                return new Result<List<TxInfo>> { Value = new List<TxInfo>() };

            var txInfos = new List<TxInfo>();
            string firstError = null;

            var txHashes = txs.Select(tx => tx.GetSha256Hash());
            foreach (var txHash in txHashes)
            {
                var result = await Get(txHash);
                if (result.Error != null)
                {
                    txInfos.Add(result.Value);
                }
                else if (firstError == null)
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
