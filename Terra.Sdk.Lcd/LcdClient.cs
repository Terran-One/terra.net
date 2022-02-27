using System;
using System.Net.Http;
using System.Web;
using Terra.Sdk.Lcd.Api;

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

            Auth = new Auth(this);
            Bank = new Bank(this);
            Distribution = new Distribution(this);
            FeeGrant = new FeeGrant(this);
            Gov = new Gov(this);
            Ibc = new Ibc(this);
            IbcTransfer = new IbcTransfer(this);
            Market = new Market(this);
        }

        public Auth Auth { get; }
        public Bank Bank { get; }
        public Distribution Distribution { get; }
        public FeeGrant FeeGrant { get; }
        public Gov Gov { get; }
        public Ibc Ibc { get; }
        public IbcTransfer IbcTransfer { get; }
        public Market Market { get; }

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
