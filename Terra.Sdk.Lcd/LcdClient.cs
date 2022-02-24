using System;
using System.Net.Http;
using System.Web;
using Terra.Sdk.Lcd.Models.Entities;

namespace Terra.Sdk.Lcd
{
    public class LcdClient
    {
        internal LcdClientConfig Config { get; }
        internal HttpClient HttpClient { get; }

        public LcdClient(LcdClientConfig config)
        {
            Config = config;
            HttpClient = new HttpClient {BaseAddress = new Uri(config.Url)};

            Bank = new Coin(this);
            FeeGrant = new Allowance(this);
        }

        public Coin Bank { get; }
        public Allowance FeeGrant { get; }

        internal string GetPaginationQueryString(string paginationKey = null, int? pageNumber = null, bool? getTotalCount = null, bool? isDescending = null)
        {
            var query = HttpUtility.ParseQueryString(string.Empty);
            query["pagination.limit"] = Config.PageSize.ToString();

            if (pageNumber.HasValue)
                query["pagination.offset"] = ((pageNumber - 1) * Config.PageSize).ToString();

            if (!string.IsNullOrWhiteSpace(paginationKey))
                query["pagination.key"] = paginationKey;

            if (getTotalCount.HasValue)
                query["pagination.count_total"] = getTotalCount.ToString();

            if (isDescending.HasValue)
                query["pagination.reverse"] = isDescending.ToString();

            var paramsString = query.ToString();
            return string.IsNullOrWhiteSpace(paramsString) ? null : $"?{paramsString}";
        }
    }
}
