using System.Collections.Generic;
using System.Threading.Tasks;
using Terra.Sdk.Lcd.Api.Parameters;
using Terra.Sdk.Lcd.Extensions;
using Terra.Sdk.Lcd.Models;
using Terra.Sdk.Lcd.Models.Entities.Tx;
using TxEntity = Terra.Sdk.Lcd.Models.Entities.Tx.Tx;

namespace Terra.Sdk.Lcd.Api
{
    public class Tx
    {
        private readonly LcdClient _client;

        internal Tx(LcdClient client)
        {
            _client = client;
        }

        public Task<Result<TxInfo>> GetTxInfo(string txHash) => new TxInfo(_client).Get(txHash);
        public Task<Result<TxEntity>> Create(IEnumerable<SignerOptions> signers, CreateTxOptions options) => new TxEntity(_client).Create(signers, options);
        public Task<Result<List<TxInfo>>> GetTxInfosByHeight(long height) => new TxInfo(_client).GetByHeight(height);
        public Task<Result<Fee>> EstimateFee(IReadOnlyCollection<SignerData> signers, CreateTxOptions options) => new Fee(_client).Estimate(signers, options);
        public Task<Result<long>> EstimateGas(TxEntity tx, decimal? gasAdjustment = null, IReadOnlyCollection<SignerData> signers = null) => tx.WithClient(_client).EstimateGas(signers, gasAdjustment);
        public string Encode(TxEntity tx) => tx.Encode();
        public TxEntity Decode(string encodedTx) => new TxEntity(_client).Decode(encodedTx);
        public string GetHash(TxEntity tx) => tx.EncodeProto().ToHexString();
        public Task<Result<BlockTxBroadcastResult>> Broadcast(TxEntity tx) => tx.WithClient(_client).Broadcast();
        public Task<Result<SyncTxBroadcastResult>> BroadcastSync(TxEntity tx) => tx.WithClient(_client).BroadcastSync();
        public Task<Result<AsyncTxBroadcastResult>> BroadcastAsync(TxEntity tx) => tx.WithClient(_client).BroadcastAsync();
        public Task<Result<TxSearchResult>> Search(TxSearchOptions options) => new Tx(_client).Search(options);
    }
}
