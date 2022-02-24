using System;
using System.Threading.Tasks;

namespace Terra.Sdk.Lcd.Models
{
    public class Result<T> where T: class
    {
        public Result(string error)
        {
            Value = null;
            NextPageQueryParams = null;
            Error = error;
        }

        public Result(T value, QueryParams nextPageQueryParams, Func<QueryParams, Task<Result<T>>> nextPage)
        {
            Value = value;
            NextPageQueryParams = nextPageQueryParams;
            NextPage = () => nextPage(NextPageQueryParams);
        }

        internal Result(T value, Pagination pagination, QueryParams queryParams, Func<QueryParams, Task<Result<T>>> nextPage)
        {
            Value = value;
            NextPageQueryParams = GetNextPageQueryParams(queryParams, pagination);
            NextPage = () => NextPageQueryParams == null ? null : nextPage(NextPageQueryParams);
        }

        /// <summary>
        /// Model value, or null if there was an error.
        /// </summary>
        public T Value { get; }

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

        private static QueryParams GetNextPageQueryParams(QueryParams queryParams, Pagination pagination)
        {
            // If neither queryParams nor pagination supplied, return null
            if (queryParams == null && string.IsNullOrWhiteSpace(pagination?.NextKey))
                return null;

            // If queryParams not supplied but pagination supplied, create new QueryParams and initialise with pagination
            if (queryParams == null)
                return new QueryParams { Key = pagination.NextKey };

            // If both supplied, update queryParams with pagination
            return queryParams.Next(pagination);
        }
    }
}
