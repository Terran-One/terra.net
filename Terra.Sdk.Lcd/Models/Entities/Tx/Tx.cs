using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using Terra.Sdk.Lcd.Api.Parameters;
using Terra.Sdk.Lcd.Extensions;
using Terra.Sdk.Lcd.Models.Entities.Account;
using Terra.Sdk.Lcd.Models.Entities.PubKey;

namespace Terra.Sdk.Lcd.Models.Entities.Tx
{
    public class Tx
    {
        private readonly LcdClient _client;

        /// <remarks>
        /// For serialization.
        /// </remarks>
        public Tx()
        {
        }

        internal Tx(LcdClient client)
        {
            _client = client;
        }

        public TxBody Body { get; set; }
        public AuthInfo AuthInfo { get; set; }
        public List<string> Signatures { get; set; }

        internal async Task<Result<Tx>> Create(IEnumerable<SignerOptions> signers, CreateTxOptions options)
        {
            var fee = options.Fee;

            var signerData = new List<SignerData>();
            foreach (var signer in signers)
            {
                var sequenceNumber = signer.SequenceNumber;
                if (!sequenceNumber.HasValue || signer.PublicKey == null)
                {
                    var account = await new Account.Account(_client).Get(signer.Address);
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
                var feeResult = await new Fee(_client).Estimate(signerData, options);
                if (feeResult.Error != null)
                {
                    fee = feeResult.Value;
                }
            }

            return new Result<Tx>
            {
                Value = new Tx
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

        internal async Task<Result<long>> EstimateGas(IReadOnlyCollection<SignerData> signers = null, decimal? gasAdjustment = null)
        {
            gasAdjustment = gasAdjustment ?? _client.Config.GasAdjustment;

            // append empty signatures if there's no signatures in tx
            var simTx = this;
            if (!Signatures.Any())
            {
                if (signers?.Any() == false)
                    return null;

                var authInfo = new AuthInfo
                {
                    SignerInfos = new List<SignerInfo>(),
                    Fee = new Fee
                    {
                        GasLimit = 0,
                        Amount = new List<Coin>()
                    }
                };

                simTx = new Tx { Body = Body, AuthInfo = authInfo, Signatures = new List<string>() };
                simTx.AppendEmptySignatures(signers);
            }

            var response = await _client.HttpClient.PostAsync(
                "/cosmos/tx/v1beta1/simulate",
                new StringContent(JsonConvert.SerializeObject(new { TyBytes = simTx.Encode() })));
            if (!response.IsSuccessStatusCode)
                return await response.GetErrorResult<long>();

            var simulateRes = JsonConvert.DeserializeAnonymousType(
                await response.Content.ReadAsStringAsync(),
                new { GasInfo = new { GasUsed = 0M } },
                _client.JsonSerializerSettings);

            return new Result<long> { Value = (long)(gasAdjustment.Value * simulateRes.GasInfo.GasUsed) };
        }

        internal void AppendEmptySignatures(IEnumerable<SignerData> signers)
        {
            foreach (var signer in signers)
            {
                SignerInfo signerInfo;
                if (signer.PublicKey != null)
                {
                    if (signer.PublicKey is LegacyAminoMultisigPublicKey legacyPublicKey)
                    {
                        signerInfo = new SignerInfo
                        {
                            PublicKey = legacyPublicKey,
                            Sequence = signer.SequenceNumber,
                            ModeInfo = new ModeInfo
                            {
                                Multi = new ModeInfo.MultiMode
                                {
                                    BitArray = CompactBitArray.FromBits(legacyPublicKey.PublicKeys.Count),
                                    ModeInfos = new List<ModeInfo>()
                                }
                            }
                        };
                    }
                    else
                    {
                        signerInfo = new SignerInfo
                        {
                            PublicKey = signer.PublicKey,
                            Sequence = signer.SequenceNumber,
                            ModeInfo = new ModeInfo
                            {
                                Single = new ModeInfo.SingleMode { Mode = ModeInfo.SignMode.Direct }
                            }
                        };
                    }
                }
                else
                {
                    signerInfo = new SignerInfo
                    {
                        PublicKey = new SimplePublicKey { Key = "" },
                        Sequence = signer.SequenceNumber,
                        ModeInfo = new ModeInfo
                        {
                            Single = new ModeInfo.SingleMode { Mode = ModeInfo.SignMode.Direct }
                        }
                    };
                }

                if (AuthInfo.SignerInfos == null) AuthInfo.SignerInfos = new List<SignerInfo>();
                if (Signatures == null) Signatures = new List<string>();

                AuthInfo.SignerInfos.Add(signerInfo);
                Signatures.Add("");
            }
        }

        internal string Encode()
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(this, _client.JsonSerializerSettings));
            return System.Convert.ToBase64String(plainTextBytes);
        }

        internal Tx Decode(string encodedTx)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(encodedTx);
            var json = Encoding.UTF8.GetString(base64EncodedBytes);
            return JsonConvert.DeserializeObject<Tx>(json, _client.JsonSerializerSettings);
        }

        internal string GetHash()
        {
            var txBytes = Encode();
            return txBytes.GetSha256Hash();
        }

        internal Task<Result<BlockTxBroadcastResult>> Broadcast() => Broadcast<BlockTxBroadcastResult>(BroadcastMode.Block);
        internal Task<Result<BlockTxBroadcastResult>> BroadcastSync() => Broadcast<BlockTxBroadcastResult>(BroadcastMode.Sync);
        internal Task<Result<BlockTxBroadcastResult>> BroadcastAsync() => Broadcast<BlockTxBroadcastResult>(BroadcastMode.Async);

        internal async Task<PaginatedGroupedResult<TxSearchResult>> Search(TxSearchOptions options, string paginationKey = null, int? pageNumber = null, bool? getTotalCount = null, bool? isDescending = null)
        {
            var queryString = string.Join("&", options.GetQueryString(), _client.GetPaginationQueryString(paginationKey, pageNumber, getTotalCount, isDescending)).TrimEnd('&');
            if (!string.IsNullOrWhiteSpace(queryString))
                queryString = $"?{queryString}";

            var response = await _client.HttpClient.GetAsync($"/cosmos/tx/v1beta1/txs{queryString}");
            if (!response.IsSuccessStatusCode)
                return await response.GetPaginatedGroupedErrorResult<TxSearchResult>();

            var value = JsonConvert.DeserializeAnonymousType(
                await response.Content.ReadAsStringAsync(),
                new
                {
                    Txs = new List<Tx>(),
                    TxResponses = new List<TxInfo>(),
                    Pagination = new
                    {
                        NextKey = "",
                        Total = (int?)null
                    }

                },
                _client.JsonSerializerSettings);

            return new PaginatedGroupedResult<TxSearchResult>
            {
                Value = new TxSearchResult
                {
                    Txs = value.Txs,
                    TxResponses = value.TxResponses,
                },
                TotalCount = value.Pagination.Total,
                NextPageKey = value.Pagination.NextKey
            };
        }

        internal async Task<Result<Tx>> GetByProposal(long proposalId)
        {
            var @params = new StringBuilder();
            @params.Append($"events={HttpUtility.UrlEncode("message.action='/cosmos.gov.v1beta1.MsgSubmitProposal'")}");
            @params.Append($"&events={HttpUtility.UrlEncode($"submit_proposal.proposal_id={proposalId}")}");

            var queryString = string.Join("&", @params, _client.GetPaginationQueryString()).TrimEnd('&');
            if (!string.IsNullOrWhiteSpace(queryString))
                queryString = $"?{queryString}";

            var response = await _client.HttpClient.GetAsync($"/cosmos/tx/v1beta1/txs{queryString}");
            if (!response.IsSuccessStatusCode)
                return await response.GetErrorResult<Tx>();

            var value = JsonConvert.DeserializeAnonymousType(
                await response.Content.ReadAsStringAsync(),
                new
                {
                    Txs = new List<Tx>(),
                    TxResponses = new List<TxInfo>(),
                    Pagination = new Pagination()

                },
                _client.JsonSerializerSettings);

            if (!value.TxResponses.Any())
                return new Result<Tx> { Error = Error.From("Failed to fetch submit_proposer tx") };

            return new Result<Tx> { Value = value.Txs.Single() };
        }

        private async Task<Result<T>> Broadcast<T>(BroadcastMode mode) where T : class
        {
            var response = await _client.HttpClient.PostAsync(
                "/cosmos/tx/v1beta1/txs",
                new StringContent(JsonConvert.SerializeObject(new { TyBytes = Encode(), Mode = mode })));
            if (!response.IsSuccessStatusCode)
                return await response.GetErrorResult<T>();

            var value = JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync(), _client.JsonSerializerSettings);
            return new Result<T> { Value = value };
        }
    }
}
