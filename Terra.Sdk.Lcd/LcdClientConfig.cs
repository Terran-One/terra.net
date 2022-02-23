using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web;
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
        public object GasPrices { get; set; } // Coins.Input

        /// <summary>
        /// Number representing the default gas adjustment value to use for fee estimation.
        /// </summary>
        public decimal GasAdjustment { get; set; }

        private HttpClient _httpClient;
        public HttpClient HttpClient => _httpClient ?? (_httpClient = new HttpClient { BaseAddress = new Uri(Url) });

        public IDictionary<string, object> ApiParams { get; set; }
        public PaginationOptions Pagination { private get; set; }

        public string GetQueryParams()
        {
            var query = HttpUtility.ParseQueryString(string.Empty);
            query["pagination.limit"] = Pagination.PageSize.ToString();
            query["pagination.offset"] = Pagination.PageNumber.ToString();
            query["pagination.key"] = Pagination.Key;
            query["pagination.count_total"] = Pagination.GetTotalCount.ToString();
            query["pagination.reverse"] = Pagination.IsAscending.ToString();

            if (ApiParams != null)
            {
                foreach (var kvp in ApiParams)
                {
                    query[kvp.Key] = kvp.Value as string ?? kvp.Value.ToString();
                }
            }

            var paramsString = query.ToString();
            return string.IsNullOrWhiteSpace(paramsString) ? null : $"?{paramsString}";
        }
    }
}
