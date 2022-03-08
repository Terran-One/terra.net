using System.Collections.Generic;
using Terra.Sdk.Lcd.Models;
using Terra.Sdk.Lcd.Models.Entities.Tx;
using Terra.Sdk.Lcd.Models.Entities.Tx.Msg;

namespace Terra.Sdk.Lcd.Api.Parameters
{
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
}