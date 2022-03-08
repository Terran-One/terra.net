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
                TotalCount = Total,
                NextPageKey = NextKey,
                NextPageNumber = pageNumber + 1
            };
        }
    }
}