using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Terra.Sdk.Lcd.Models;
using Terra.Sdk.Lcd.Models.Entities;

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

        public async Task<Result<Models.Entities.Tx>> Create(SignerOptions signerOptions, CreateTxOptions createTxOptions)
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

        public async Task<Result<long>> EstimateGas(Tx tx, decimal? gasAdjustment = null, IEnumerable<SignerData> signers = null)
        {
            throw new NotImplementedException();
        }

        public string Encode(Tx tx)
        {
            throw new NotImplementedException();
        }

        public Tx Decode(string encodedTx)
        {
            throw new NotImplementedException();
        }

        public Task<string> Hash(Tx tx)
        {
            throw new NotImplementedException();
        }

        public Task<Result<BlockTxBroadcastResult>> Broadcast(Tx tx)
        {
            throw new NotImplementedException();
        }

        public Task<Result<SyncTxBroadcastResult>> BroadcastSync(Tx tx)
        {
            throw new NotImplementedException();
        }

        public Task<Result<AsyncTxBroadcastResult>> BroadcastAsync(Tx tx)
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