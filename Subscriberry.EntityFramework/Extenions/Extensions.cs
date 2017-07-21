namespace Subscriberry.EntityFramework.Extenions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.EntityFrameworkCore;
    using Subscriberry.EntityFramework.DataAccess;
    using Subscriberry.EntityFramework.Helper;

    internal static class Extensions
    {
        public static void AddConfiguration<TEntity>(this ModelBuilder modelBuilder, DbEntityConfiguration<TEntity> entityConfiguration)
            where TEntity : class
        {
            modelBuilder.Entity<TEntity>(entityConfiguration.Configure);
        }

        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            var seenKeys = new HashSet<TKey>();
            foreach (var element in source)
            {
                if (seenKeys.Add(keySelector(element)))
                {
                    yield return element;
                }
            }
        }

        public static PaginatedData<T> Paginate<T>(this IQueryable<T> query, int pageNum, int pageSize)
        {
            if (pageSize <= 0)
            {
                pageSize = 10;
            }

            //Total result count
            var rowsCount = query.Count();

            //If page number should be > 0 else set to first page
            if (rowsCount <= pageSize || pageNum <= 0)
            {
                pageNum = 1;
            }

            //Calculate number of rows to skip on page size
            var excludedRows = (pageNum - 1) * pageSize;

            return new PaginatedData<T>
            {
                Results = query.Skip(excludedRows).Take(Math.Min(pageSize, rowsCount)).ToArray(),
                TotalCount = rowsCount
            };
        }
    }
}