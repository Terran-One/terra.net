using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProtoBuf;
using Terra.Sdk.Lcd.Api.Parameters;

namespace Terra.Sdk.Lcd.Models.Entities.Tx
{
    [ProtoContract]
    public class Fee
    {
        private readonly LcdClient _client;

        /// <remarks>
        /// For serialization.
        /// </remarks>
        public Fee()
        {
        }

        internal Fee(LcdClient client)
        {
            _client = client;
        }

        [ProtoMember(1, Name = "amount")] public List<Coin> Amount { get; set; }
        [ProtoMember(2, Name = "gas_limit")] public long GasLimit { get; set; }
        [ProtoMember(3, Name = "payer")] public string Payer { get; set; }
        [ProtoMember(4, Name = "granter")] public string Granter { get; set; }

        internal async Task<Result<Fee>> Estimate(IEnumerable<SignerData> signers, CreateTxOptions options)
        {
            var gasPrices = options.GasPrices ?? _client.Config.GasPrices;
            var gasAdjustment = options.GasAdjustment ?? _client.Config.GasAdjustment;
            var feeDenoms = options.FeeDenoms?.ToArray() ?? new[] {"uusd"};
            var rawGasValue = options.Gas;

            List<Coin> gasPricesCoins = null;
            if (gasPrices?.Any() == true)
            {
                gasPricesCoins = gasPrices;

                if (feeDenoms.Any())
                {
                    var gasPricesCoinsFiltered = gasPricesCoins.Where(c => feeDenoms.Contains(c.Denom)).ToList();
                    if (gasPricesCoinsFiltered.Any())
                        gasPricesCoins = gasPricesCoinsFiltered;
                }
            }

            var txBody = new TxBody {Messages = options.Msgs.ToList(), Memo = options.Memo ?? ""};
            var authInfo = new AuthInfo {SignerInfos = new List<SignerInfo>(), Fee = new Fee {GasLimit = 0, Amount = new List<Coin>()}};
            var tx = new Tx(_client) {Body = txBody, AuthInfo = authInfo, Signatures = new List<string>()};

            // fill empty signature
            tx.AppendEmptySignatures(signers);

            // simulate gas
            long gasLimit;
            if (rawGasValue == null || rawGasValue == "auto" || rawGasValue == "0")
            {
                var gasResult = await tx.EstimateGas(gasAdjustment: gasAdjustment);
                if (gasResult.Error != null)
                    return new Result<Fee> {Error = gasResult.Error};

                gasLimit = gasResult.Value;
            }
            else
            {
                gasLimit = long.Parse(rawGasValue);
            }

            var feeAmount = gasPricesCoins != null
                ? gasPricesCoins.Select(c => c.Multiply(gasLimit).ToIntCeilCoin()).ToList()
                : new List<Coin> {new Coin("uusd", 0)};

            return new Result<Fee>
            {
                Value = new Fee
                {
                    GasLimit = gasLimit,
                    Amount = feeAmount,
                    Payer = "",
                    Granter = ""
                }
            };
        }

        internal object ToAmino() => new {Gas = GasLimit.ToString(), Amount};
    }
}
