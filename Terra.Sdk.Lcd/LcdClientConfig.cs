using System;
using System.Net.Http;

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
        public object GasPrices { get; set; } // Coins.Input

        /// <summary>
        /// Number representing the default gas adjustment value to use for fee estimation.
        /// </summary>
        public decimal GasAdjustment { get; set; }

        /// <summary>
        /// Http client connecting to <see cref="Url" />.
        /// </summary>
        internal HttpClient HttpClient => _httpClient ?? (_httpClient = new HttpClient { BaseAddress = new Uri(Url) });
        private HttpClient _httpClient;
    }
}
