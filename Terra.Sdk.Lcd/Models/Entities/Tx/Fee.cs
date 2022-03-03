using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Terra.Sdk.Lcd.Api;
using Terra.Sdk.Lcd.Api.Parameters;

namespace Terra.Sdk.Lcd.Models.Entities.Tx
{
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

        public long GasLimit { get; set; }
        public List<Coin> Amount { get; set; }
        public string Payer { get; set; }
        public string Granter { get; set; }

        public async Task<Result<Fee>> Estimate(IEnumerable<SignerData> signers, CreateTxOptions options)
        {
            var gasPrices = options.GasPrices ?? _client.Config.GasPrices;
            var gasAdjustment = options.GasAdjustment ?? _client.Config.GasAdjustment;
            var feeDenoms = options.FeeDenoms?.ToArray() ?? new[] { "uusd" };
            var gas = options.Gas;

            List<Coin> gasPricesCoins = null;
            if (gasPrices?.Any() == true)
            {
                gasPricesCoins = gasPrices;

                if (feeDenoms.Any())
                {
                    var gasPricesCoinsFiltered = gasPricesCoins.Where(c => feeDenoms.Contains(c.Denom)).ToList();
                    if (gasPricesCoinsFiltered.Any())
                    {
                        gasPricesCoins = gasPricesCoinsFiltered;
                    }
                }
            }

            var txBody = new TxBody { Messages = options.Msgs.ToList(), Memo = options.Memo ?? "" };
            var authInfo = new AuthInfo { SignerInfos = new List<SignerInfo>(), Fee = new Fee { GasLimit = 0, Amount = new List<Coin>()}};
            var tx = new Tx(_client) { Body = txBody, AuthInfo = authInfo, Signatures = new List<string>()};

            // fill empty signature
            tx.AppendEmptySignatures(signers);

            // simulate gas
            if (gas == null || gas == "auto" || gas == "0")
                gas = (await tx.EstimateGas(gasAdjustment: gasAdjustment)).ToString();

            var feeAmount = gasPricesCoins != null
                ? gasPricesCoins.Select(c => c.Multiply(decimal.Parse(gas)).ToIntCeilCoin()).ToList()
                : new List<Coin> { new Coin("uusd", 0) };

            return new Result<Fee>
            {
                Value = new Fee
                {
                    GasLimit = int.Parse(gas),
                    Amount = feeAmount,
                    Payer = "",
                    Granter = ""

                }
            };
        }

        internal object ToAmino() => new { Gas = GasLimit.ToString(), Amount };
    }
}
