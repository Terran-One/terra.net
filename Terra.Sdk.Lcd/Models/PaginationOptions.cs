using System.Web;

namespace Terra.Sdk.Lcd.Models
{
    public class PaginationOptions
    {
        public int PageSize { get; set; } = 100;
        public int PageNumber { get; set; }
        public string Key { get; set; } // ToDo: what is this?
        public bool GetTotalCount { get; set; }
        public bool IsAscending { get; set; } = true;

        public override string ToString()
        {
            var query = HttpUtility.ParseQueryString(string.Empty);
            query["pagination.limit"] = PageSize.ToString();
            query["pagination.offset"] = (PageNumber * PageSize).ToString();
            query["pagination.key"] = Key;
            query["pagination.count_total"] = GetTotalCount.ToString();
            query["pagination.reverse"] = (!IsAscending).ToString();

            var paramsString = query.ToString();
            return string.IsNullOrWhiteSpace(paramsString) ? null : $"?{paramsString}";
        }
    }
}
