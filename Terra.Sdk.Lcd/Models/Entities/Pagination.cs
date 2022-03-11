using System.Collections.Generic;

namespace Terra.Sdk.Lcd.Models.Entities
{
    public class Pagination
    {
        public string NextKey { get; set; }
        public int Total { get; set; }

        public PaginatedResult<T> BuildResult<T>(List<T> value, int? pageNumber)
        {
            return new PaginatedResult<T>
            {
                Value = value,
                TotalCount = Total == 0 ? (int?)null : Total,
                NextPageKey = NextKey,
                NextPageNumber = string.IsNullOrWhiteSpace(NextKey) && Total > 0 ? (pageNumber ?? 1) + 1 : (int?)null
            };
        }
    }
}
