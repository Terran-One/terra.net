using System.Collections.Specialized;
using System.Web;

namespace Terra.Sdk.Lcd.Models
{
    public class QueryParams
    {
        /// <summary>
        /// Total number of records to return in a page. Defaults to 10;
        /// </summary>
        /// <remarks>
        /// Used in conjunction with <see cref="PageNumber"/>. Should be used only when <see cref="Key"/> is unavailable.
        /// It is less efficient than using key. Only one of PageSize/PageNumber or Key should be set.
        /// </remarks>
        public int PageSize { get; set; } = 10;

        /// <summary>
        /// Page number to return. Defaults to 1.
        /// </summary>
        /// <remarks>
        /// Used in conjunction with <see cref="PageSize"/>. Should be used only when <see cref="Key"/> is unavailable.
        /// It is less efficient than using key. Only one of PageSize/PageNumber or Key should be set.
        /// </remarks>
        public int PageNumber { get; set; } = 1;

        /// <summary>
        /// A value returned in <see cref="Pagination.NextKey"/> to begin querying the next page most efficiently.
        /// For the first page/query, leave this empty. Only one of offset or key should be set.
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Set to true to indicate that the result set should include a count of the total number of items available for pagination in UIs.
        /// Only respected when <see cref="PageNumber"/> is used. It is ignored when <see cref="Key"/> is set.
        /// </summary>
        public bool GetTotalCount { get; set; }

        /// <summary>
        /// Set to false if results are to be returned in the descending order. Defaults to true.
        /// </summary>
        public bool IsAscending { get; set; } = true;

        /// <summary>
        /// Any other parameters to be passed to the API call.
        /// </summary>
        public NameValueCollection CustomParams { get; set; }

        /// <returns>
        /// A query string (including the '?' prefix), to be appended to a URL.
        /// If no values are specified, returns null.
        /// </returns>
        public override string ToString()
        {
            var query = HttpUtility.ParseQueryString(string.Empty);
            query["pagination.limit"] = PageSize.ToString();
            query["pagination.offset"] = ((PageNumber - 1) * PageSize).ToString();
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
