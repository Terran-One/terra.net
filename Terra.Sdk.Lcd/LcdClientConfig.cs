using System.Collections.Generic;
using Terra.Sdk.Lcd.Models;

namespace Terra.Sdk.Lcd
{
    public class LcdClientConfig
    {
        /// <summary>
        /// The base URL to which LCD requests will be made.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Chain ID of the blockchain to connect to.
        /// </summary>
        public string ChainId { get; set; }

        /// <summary>
        /// Coins representing the default gas prices to use for fee estimation.
        /// </summary>
        public List<Coin> GasPrices { get; set; }

        /// <summary>
        /// Number representing the default gas adjustment value to use for fee estimation.
        /// </summary>
        public decimal GasAdjustment { get; set; }

        /// <summary>
        /// Total number of records to return in a page. Defaults to 10;
        /// </summary>
        public int PageSize { get; set; } = 10;
    }
}
