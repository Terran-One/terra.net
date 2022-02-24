using System.Web;

namespace Terra.Sdk.Lcd.Models
{
    public class QueryParams
    {
        /// <summary>
        /// Total number of records to return in a page. Defaults to 10;
        /// </summary>
        public int PageSize { get; set; } = 10;

        /// <summary>
        /// Page number to return.
        /// </summary>
        /// <remarks>
        /// Should be used only when <see cref="Key"/> is unavailable, as it is less efficient than using Key.
        /// Only one of PageNumber or Key should be set.
        /// </remarks>
        public int? PageNumber { get; set; }

        /// <summary>
        /// A value returned in <see cref="Pagination.NextKey"/> to begin querying the next page most efficiently.
        /// For the first page/query, leave this empty. Only one of offset or key should be set.
        /// </summary>
        /// <remarks>
        /// Key is the preferred way of performing pagination. Only one of PageNumber or Key should be set.
        /// </remarks>
        public string Key { get; set; }

        /// <summary>
        /// Set to true to indicate that the result set should include a count of the total number of items available for pagination in UIs.
        /// Only respected when <see cref="PageNumber"/> is used. It is ignored when <see cref="Key"/> is set.
        /// </summary>
        public bool? GetTotalCount { get; set; }

        /// <summary>
        /// Set to true if results are to be returned in the descending order.
        /// </summary>
        public bool? IsDescending { get; set; } = true;

        /// <returns>
        /// A query string (including the '?' prefix), to be appended to a URL.
        /// If no values are specified, returns null.
        /// </returns>
        public override string ToString()
        {
            var query = HttpUtility.ParseQueryString(string.Empty);
            query["pagination.limit"] = PageSize.ToString();

            if (PageNumber.HasValue)
                query["pagination.offset"] = ((PageNumber - 1) * PageSize).ToString();

            if (!string.IsNullOrWhiteSpace(Key))
                query["pagination.key"] = Key;

            if (GetTotalCount.HasValue)
                query["pagination.count_total"] = GetTotalCount.ToString();

            if (IsDescending.HasValue)
                query["pagination.reverse"] = IsDescending.ToString();

            var paramsString = query.ToString();
            return string.IsNullOrWhiteSpace(paramsString) ? null : $"?{paramsString}";
        }

        internal QueryParams Next(string nextKey)
        {
            var next = (QueryParams)MemberwiseClone();
            next.Key = nextKey;

            if (string.IsNullOrWhiteSpace(next.Key) && PageNumber.HasValue)
                next.PageNumber = PageNumber + 1;

            return next;
        }
    }
}
