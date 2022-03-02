using System.Collections.Generic;

namespace Terra.Sdk.Lcd.Models
{
    public class PaginatedResult<TEntity>
    {
        /// <summary>
        /// Returned entities.
        /// </summary>
        public List<TEntity> Value { get; set; }

        /// <summary>
        /// Total record count, if returned.
        /// </summary>
        public int? TotalCount { get; set; }

        /// <summary>
        /// Key for retrieving the next page.
        /// </summary>
        /// <remarks>
        /// Specify only one of <see cref="NextPageKey"/> or <see cref="NextPageNumber"/>.
        /// </remarks>
        public string NextPageKey { get; set; }

        /// <summary>
        /// Next page number.
        /// </summary>
        /// <remarks>
        /// Specify only one of <see cref="NextPageKey"/> or <see cref="NextPageNumber"/>.
        /// </remarks>
        public int? NextPageNumber { get; set; }

        /// <summary>
        /// If an error has occurred whilst retrieving the model,
        /// the state will be stored here.
        /// </summary>
        public string Error { get; set; }
    }

    public class PaginatedGroupedResult<TEntity>
    {
        /// <summary>
        /// Returned entity.
        /// </summary>
        public TEntity Value { get; set; }

        /// <summary>
        /// Total record count, if returned.
        /// </summary>
        public int? TotalCount { get; set; }

        /// <summary>
        /// Key for retrieving the next page.
        /// </summary>
        /// <remarks>
        /// Specify only one of <see cref="NextPageKey"/> or <see cref="NextPageNumber"/>.
        /// </remarks>
        public string NextPageKey { get; set; }

        /// <summary>
        /// Next page number.
        /// </summary>
        /// <remarks>
        /// Specify only one of <see cref="NextPageKey"/> or <see cref="NextPageNumber"/>.
        /// </remarks>
        public int? NextPageNumber { get; set; }

        /// <summary>
        /// If an error has occurred whilst retrieving the model,
        /// the state will be stored here.
        /// </summary>
        public string Error { get; set; }
    }
}
