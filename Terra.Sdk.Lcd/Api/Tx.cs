using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Terra.Sdk.Lcd.Models;
using Terra.Sdk.Lcd.Models.Entities.Account;
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

        public async Task<Result<Models.Entities.Tx.Tx>> Create(IEnumerable<SignerOptions> signers, CreateTxOptions options)
        {
            var fee = options.Fee;

            var signerData = new List<SignerData>();
            foreach (var signer in signers)
            {
                var sequenceNumber = signer.SequenceNumber;
                if (!sequenceNumber.HasValue || signer.PublicKey == null)
                {
                    var account = await new Account(_client).Get(signer.Address);
                    if (account.Error != null)
                    {
                        var baseAccount = account.Value is LazyGradedVestingAccount vestingAccount
                            ? vestingAccount.BaseVestingAccount
                            : (BaseAccount)account.Value;

                        if (!sequenceNumber.HasValue)
                        {
                            sequenceNumber = baseAccount.Sequence;
                        }

                        if (signer.PublicKey == null)
                        {
                            signer.PublicKey = baseAccount.PubKey;
                        }
                    }
                }

                Debug.Assert(sequenceNumber != null, $"{nameof(sequenceNumber)} != null");
                signerData.Add(new SignerData { SequenceNumber = sequenceNumber.Value, PublicKey = signer.PublicKey });
            }

            if (options.Fee == null)
            {
                var feeResult = await EstimateFee(signerData, options);
                if (feeResult.Error != null)
                {
                    fee = feeResult.Value;
                }
            }

            return new Result<Models.Entities.Tx.Tx>
            {
                Value = new Models.Entities.Tx.Tx
                {
                    Body = new TxBody
                    {
                        Messages = options.Msgs.ToList(),
                        Memo = options.Memo ?? "",
                        TimeoutHeight = options.TimeoutHeight ?? 0
                    },
                    AuthInfo = new AuthInfo
                    {
                        SignerInfos = new List<SignerInfo>(),
                        Fee = fee
                    },
                    Signatures = new List<string>()
                }
            };
        }

        public Task<Result<List<TxInfo>>> GetTxInfoByHeight(long height) => new TxInfo(_client).GetByHeight(height);

        public async Task<Result<Fee>> EstimateFee(IEnumerable<SignerData> signerData, CreateTxOptions options)
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

        public Task<string> GetHash(Models.Entities.Tx.Tx tx)
        {
            throw new NotImplementedException();
        }

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
        public decimal GasAdjustment { get; set; }
        public IEnumerable<string> FeeDenoms { get; set; }
        public long? TimeoutHeight { get; set; }
    }

    public class TxSearchOptions
    {
        public IEnumerable<TxEvent> Events { get; set; }
    }
}
