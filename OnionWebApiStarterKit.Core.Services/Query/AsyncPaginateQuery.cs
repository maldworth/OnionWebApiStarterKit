using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace OnionWebApiStarterKit.Core.Services.Query
{
    public class AsyncPaginateQuery<TEntity> : IAsyncRequest<PaginatedList<TEntity>>, IPaginateQuery<TEntity>, IFilterQuery<TEntity>, IIncludeQuery<TEntity>
        where TEntity : class
    {
        public const int PAGE_INDEX_DEFAULT = 1;
        public const int PAGE_SIZE_MIN = 1;
        public const int PAGE_SIZE_MAX = 100;

        public int PageIndex { get; private set; }
        public int PageSize { get; private set; }
        public Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> OrderBy { get; private set; }
        public Expression<Func<TEntity, bool>> Predicate { get; private set; }
        public Expression<Func<TEntity, object>>[] IncludeProperties { get; private set; }

        public AsyncPaginateQuery(
            int pageIndex,
            int pageSize,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy,
            Expression<Func<TEntity, bool>> predicate = null,
            params Expression<Func<TEntity, object>>[] includeProperties
            )
        {
            PageIndex = Math.Max(pageIndex, PAGE_INDEX_DEFAULT);
            PageSize = Math.Min(Math.Max(pageSize, PAGE_SIZE_MIN), PAGE_SIZE_MAX);
            OrderBy = orderBy;
            Predicate = predicate;
            IncludeProperties = includeProperties;
        }
    }
}
