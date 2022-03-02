using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Terra.Sdk.Lcd.Models;
using Terra.Sdk.Lcd.Models.Entities.PubKey;
using Terra.Sdk.Lcd.Models.Entities.Tx;
using Terra.Sdk.Lcd.Models.Entities.Tx.Msg;

namespace Terra.Sdk.Lcd.Api
{
    public class Tx
    {
        private readonly LcdClient _client;

        public Tx(LcdClient client)
        {
            _client = client;
        }

        public Task<Result<TxInfo>> GetTxInfo(string txHash) => new TxInfo(_client).Get(txHash);
        public Task<Result<Models.Entities.Tx.Tx>> Create(IEnumerable<SignerOptions> signers, CreateTxOptions options) => new Models.Entities.Tx.Tx(_client).Create(signers, options);
        public Task<Result<List<TxInfo>>> GetTxInfoByHeight(long height) => new TxInfo(_client).GetByHeight(height);
        public Task<Result<Fee>> EstimateFee(IEnumerable<SignerData> signers, CreateTxOptions options) => new Fee(_client).Estimate(signers, options);
        public Task<Result<long>> EstimateGas(Models.Entities.Tx.Tx tx, decimal? gasAdjustment = null, IReadOnlyCollection<SignerData> signers = null) => tx.EstimateGas(signers, gasAdjustment);
        public string Encode(Models.Entities.Tx.Tx tx) => tx.Encode();
        public Models.Entities.Tx.Tx Decode(string encodedTx) => new Models.Entities.Tx.Tx(_client).Decode(encodedTx);
        public string GetHash(Models.Entities.Tx.Tx tx) => tx.Encode();

        public Task<Result<BlockTxBroadcastResult>> Broadcast(Models.Entities.Tx.Tx tx)
        {
            throw new NotImplementedException();
        }

        public Task<Result<BlockTxBroadcastResult>> BroadcastSync(Models.Entities.Tx.Tx tx)
        {
            throw new NotImplementedException();
        }

        public Task<Result<AsyncTxBroadcastResult>> BroadcastAsync(Models.Entities.Tx.Tx tx)
        {
            throw new NotImplementedException();
        }

        public Task<Result<TxSearchResult>> Search(TxSearchOptions options)
        {
            throw new NotImplementedException();
        }
    }

    public class SignerOptions
    {
        public string Address { get; set; }
        public long? SequenceNumber { get; set; }
        public PublicKey PublicKey { get; set; }
    }

    public class SignerData
    {
        public long SequenceNumber { get; set; }
        public PublicKey PublicKey { get; set; }
    }

    public class CreateTxOptions
    {
        public IEnumerable<Msg> Msgs { get; set; }
        public Fee Fee { get; set; }
        public string Memo { get; set; }
        public string Gas { get; set; }
        public List<Coin> GasPrices { get; set; }
        public decimal? GasAdjustment { get; set; }
        public IEnumerable<string> FeeDenoms { get; set; }
        public long? TimeoutHeight { get; set; }
    }

    public class TxSearchOptions
    {
        public IEnumerable<TxEvent> Events { get; set; }
    }
}
