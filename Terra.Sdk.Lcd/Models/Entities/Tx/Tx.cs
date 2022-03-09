using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using ProtoBuf;
using Terra.Sdk.Lcd.Api.Parameters;
using Terra.Sdk.Lcd.Extensions;
using Terra.Sdk.Lcd.Models.Entities.Account;
using Terra.Sdk.Lcd.Models.Entities.PubKey;

namespace Terra.Sdk.Lcd.Models.Entities.Tx
{
    [ProtoContract]
    public class Tx
    {
        private LcdClient _client;

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

        internal Tx WithClient(LcdClient client)
        {
            _client = client;
            return this;
        }

        [ProtoMember(1, Name = "body")]
        public TxBody Body { get; set; }

        [ProtoMember(2, Name = "auth_info")]
        public AuthInfo AuthInfo { get; set; }

        public List<string> Signatures
        {
            get => _signatures;
            set
            {
                _signatures = value;
                _protoSignatures = _signatures?.Select(Encoding.UTF8.GetBytes).ToList();
            }
        }
        private List<string> _signatures;

        /// <remarks>
        /// For protobuf serialization.
        /// </remarks>
        [JsonIgnore]
        [ProtoMember(3, Name = "signatures")]
        public List<byte[]> ProtoSignatures
        {
            get => _protoSignatures;
            set
            {
                _protoSignatures = value;
                _signatures = _protoSignatures?.Select(Encoding.UTF8.GetString).ToList();
            }
        }
        private List<byte[]> _protoSignatures;

        internal void AddSignature(string signature)
        {
            Signatures.Add(signature);
            ProtoSignatures.Add(Encoding.UTF8.GetBytes(signature));
        }

        internal void AddSignatures(IReadOnlyCollection<string> signatures)
        {
            Signatures.AddRange(signatures);
            ProtoSignatures.AddRange(signatures.Select(Encoding.UTF8.GetBytes));
        }

        public string Encode()
        {
            var plainTextBytes = this.EncodeProto();
            return Convert.ToBase64String(plainTextBytes);
        }

        public Tx Decode(string encodedTx)
        {
            var bytes = Convert.FromBase64String(encodedTx);
            return bytes.DecodeProto<Tx>();
        }

        public string GetHash()
        {
            var txBytes = Encode();
            return txBytes.GetSha256Hash();
        }

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
                            : (BaseAccount) account.Value;

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
                signerData.Add(new SignerData {SequenceNumber = sequenceNumber.Value, PublicKey = signer.PublicKey});
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
                if (signers?.Any() != true)
                    return new Result<long> {Error = new Error {Message = "Cannot append signature"}};

                var authInfo = new AuthInfo
                {
                    SignerInfos = new List<SignerInfo>(),
                    Fee = new Fee
                    {
                        GasLimit = 0,
                        Amount = new List<Coin>()
                    }
                };

                simTx = new Tx {Body = Body, AuthInfo = authInfo, Signatures = new List<string>()};
                simTx.AppendEmptySignatures(signers);
            }

            var response = await _client.HttpClient.PostAsync(
                "/cosmos/tx/v1beta1/simulate",
                new StringContent(JsonConvert.SerializeObject(new {TxBytes = simTx.Encode()}, Global.JsonSerializerSettings)));
            if (!response.IsSuccessStatusCode)
                return await response.GetErrorResult<long>();

            var simulateRes = JsonConvert.DeserializeAnonymousType(
                await response.Content.ReadAsStringAsync(),
                new {GasInfo = new {GasUsed = 0M}},
                Global.JsonSerializerSettings);

            return new Result<long> {Value = (long) (gasAdjustment.Value * simulateRes.GasInfo.GasUsed)};
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
                                Multi = new Multi
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
                                Single = new Single {Mode = SignMode.Direct}
                            }
                        };
                    }
                }
                else
                {
                    signerInfo = new SignerInfo
                    {
                        PublicKey = new SimplePublicKey {Key = ""},
                        Sequence = signer.SequenceNumber,
                        ModeInfo = new ModeInfo
                        {
                            Single = new Single {Mode = SignMode.Direct}
                        }
                    };
                }

                if (AuthInfo.SignerInfos == null) AuthInfo.SignerInfos = new List<SignerInfo>();
                if (Signatures == null) Signatures = new List<string>();

                AuthInfo.SignerInfos.Add(signerInfo);
                AddSignature("");
            }
        }

        internal Task<Result<BlockTxBroadcastResult>> Broadcast() => Broadcast<BlockTxBroadcastResult>(BroadcastMode.Block);
        internal Task<Result<SyncTxBroadcastResult>> BroadcastSync() => Broadcast<SyncTxBroadcastResult>(BroadcastMode.Sync);
        internal Task<Result<AsyncTxBroadcastResult>> BroadcastAsync() => Broadcast<AsyncTxBroadcastResult>(BroadcastMode.Async);

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
                        Total = (int?) null
                    }
                },
                Global.JsonSerializerSettings);

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
                Global.JsonSerializerSettings);

            if (!value.TxResponses.Any())
                return new Result<Tx> {Error = new Error {Message = "Failed to fetch submit_proposer tx"}};

            return new Result<Tx> {Value = value.Txs.Single()};
        }

        private async Task<Result<T>> Broadcast<T>(BroadcastMode mode) where T : BlockTxBroadcastResult, new()
        {
            var response = await _client.HttpClient.PostAsync(
                "/cosmos/tx/v1beta1/txs",
                new StringContent(JsonConvert.SerializeObject(new {TxBytes = Encode(), Mode = mode}, Global.JsonSerializerSettings)));
            if (!response.IsSuccessStatusCode)
                return await response.GetErrorResult<T>();

            var value = JsonConvert.DeserializeAnonymousType(
                await response.Content.ReadAsStringAsync(),
                new { TxResponse = new T() },
                Global.JsonSerializerSettings);
            return new Result<T> {Value = value.TxResponse};
        }
    }
}
