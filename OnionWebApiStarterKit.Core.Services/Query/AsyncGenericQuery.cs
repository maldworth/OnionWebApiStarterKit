using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace OnionWebApiStarterKit.Core.Services.Query
{
    public class AsyncGenericQuery<TEntity> : IAsyncRequest<IEnumerable<TEntity>>, IFilterQuery<TEntity>, IOrderByQuery<TEntity>, IIncludeQuery<TEntity>, ITakeQuery
        where TEntity : class
    {
        public const int PAGE_SIZE_MIN = 1;
        public const int PAGE_SIZE_MAX = 100;

        public int PageSize { get; private set; }
        public Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> OrderBy { get; private set; }
        public Expression<Func<TEntity, bool>> Predicate { get; private set; }
        public Expression<Func<TEntity, object>>[] IncludeProperties { get; private set; }

        public AsyncGenericQuery(
            int pageSize,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Expression<Func<TEntity, bool>> predicate = null,
            params Expression<Func<TEntity, object>>[] includeProperties
            )
        {
            PageSize = Math.Min(Math.Max(pageSize, PAGE_SIZE_MIN), PAGE_SIZE_MAX);
            OrderBy = orderBy;
            Predicate = predicate;
            IncludeProperties = includeProperties;
        }
    }
}
