using System;
using System.Threading.Tasks;

namespace Terra.Sdk.Lcd.Models
{
    public class Result<T> where T: class
    {
        public Result(string error)
        {
            Error = error;
        }

        public Result(T value, int? totalCount, QueryParams nextPageQueryParams, Func<QueryParams, Task<Result<T>>> nextPage)
        {
            Value = value;
            TotalCount = totalCount;
            NextPageQueryParams = nextPageQueryParams;
            NextPage = () => nextPageQueryParams == null ? null : nextPage(NextPageQueryParams);
        }

        internal Result(T value, string nextKey, int? totalCount, QueryParams queryParams, Func<QueryParams, Task<Result<T>>> nextPage)
        {
            Value = value;
            TotalCount = totalCount;
            NextPageQueryParams = GetNextPageQueryParams(queryParams, nextKey);
            NextPage = () => NextPageQueryParams == null ? null : nextPage(NextPageQueryParams);
        }

        /// <summary>
        /// Model value, or null if there was an error.
        /// </summary>
        public T Value { get; }

        /// <summary>
        /// Total record count, if returned.
        /// </summary>
        public int? TotalCount { get; }

        /// <summary>
        /// Query parameters for retrieving the next page.
        /// </summary>
        public QueryParams NextPageQueryParams { get; }

        /// <summary>
        /// If an error has occurred whilst retrieving the model,
        /// the state will be stored here.
        /// </summary>
        public string Error { get; }

        /// <summary>
        /// Gets the next page.
        /// </summary>
        public Func<Task<Result<T>>> NextPage { get; }

        private static QueryParams GetNextPageQueryParams(QueryParams queryParams, string nextKey)
        {
            // If neither queryParams nor nextKey supplied, return null
            if (queryParams == null && string.IsNullOrWhiteSpace(nextKey))
                return null;

            // If queryParams not supplied but pagination supplied, create new QueryParams and initialise with nextKey
            if (queryParams == null)
                return new QueryParams { Key = nextKey };

            // If both supplied, update queryParams with nextKey
            return queryParams.Next(nextKey);
        }
    }
}
