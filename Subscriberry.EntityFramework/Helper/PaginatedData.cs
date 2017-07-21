namespace Subscriberry.EntityFramework.Helper
{
    using System.Collections.Generic;

    internal class PaginatedData<T>
    {
        public IEnumerable<T> Results { get; set; }
        public int TotalCount { get; set; }
    }
}