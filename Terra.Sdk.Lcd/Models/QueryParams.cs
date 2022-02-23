using System.Collections.Specialized;
using System.Web;

namespace Terra.Sdk.Lcd.Models
{
    public class QueryParams
    {
        public int PageSize { get; set; } = 100;
        public int PageNumber { get; set; }
        public string Key { get; set; } // ToDo: what is this?
        public bool GetTotalCount { get; set; }
        public bool IsAscending { get; set; } = true;

        public NameValueCollection CustomParams { get; set; }

        public override string ToString()
        {
            var query = HttpUtility.ParseQueryString(string.Empty);
            query["pagination.limit"] = PageSize.ToString();
            query["pagination.offset"] = PageNumber.ToString();
            query["pagination.key"] = Key;
            query["pagination.count_total"] = GetTotalCount.ToString();
            query["pagination.reverse"] = IsAscending.ToString();

            if (CustomParams != null)
                query.Add(CustomParams);

            var paramsString = query.ToString();
            return string.IsNullOrWhiteSpace(paramsString) ? null : $"?{paramsString}";
        }
    }
}
