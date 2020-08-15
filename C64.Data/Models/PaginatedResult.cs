using System;
using System.Collections.Generic;

namespace C64.Data.Models
{
    public class PaginatedResult<T>
    {
        public int TotalNumberOfRecords { get; set; }
        public int PageSize { get; set; } = 10;
        public int CurrentPage { get; set; }

        public int NumberOfPages => ((TotalNumberOfRecords - 1) / PageSize) + 1;
        public int StartRecords => (CurrentPage - 1) * PageSize + 1;

        public int EndRecord => Math.Min(CurrentPage * PageSize, TotalNumberOfRecords);
        public IEnumerable<T> Data { get; set; } = new HashSet<T>();
    }
}