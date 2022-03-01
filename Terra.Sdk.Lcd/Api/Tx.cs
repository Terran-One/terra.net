using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Terra.Sdk.Lcd.Models;
using Terra.Sdk.Lcd.Models.Entities.Tx;

namespace Terra.Sdk.Lcd.Api
{
    public class Tx
    {
        private readonly LcdClient _client;

        public Tx(LcdClient client)
        {
            _client = client;
        }

        public async Task<Result<TxInfo>> GetTxInfo(string txHash)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<Models.Entities.Tx.Tx>> Create(SignerOptions signerOptions, CreateTxOptions createTxOptions)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<List<TxInfo>>> GetTxInfoByHeight(long height)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<Fee>> EstimateFee(IEnumerable<SignerData> signerData, SignerOptions options)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<long>> EstimateGas(Models.Entities.Tx.Tx tx, decimal? gasAdjustment = null, IEnumerable<SignerData> signers = null)
        {
            throw new NotImplementedException();
        }

        public string Encode(Models.Entities.Tx.Tx tx)
        {
            throw new NotImplementedException();
        }

        public Models.Entities.Tx.Tx Decode(string encodedTx)
        {
            throw new NotImplementedException();
        }

        public Task<string> Hash(Models.Entities.Tx.Tx tx)
        {
            throw new NotImplementedException();
        }

        public Task<Result<BlockTxBroadcastResult>> Broadcast(Models.Entities.Tx.Tx tx)
        {
            throw new NotImplementedException();
        }

        public Task<Result<SyncTxBroadcastResult>> BroadcastSync(Models.Entities.Tx.Tx tx)
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
        
    }

    public class SignerData
    {
        
    }

    public class CreateTxOptions
    {
        
    }

    public class TxSearchOptions
    {
        
    }
}