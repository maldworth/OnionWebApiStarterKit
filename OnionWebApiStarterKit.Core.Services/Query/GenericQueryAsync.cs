using MediatR;
using OnionWebApiStarterKit.Core.Services.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace OnionWebApiStarterKit.Core.Services.Query
{
    public class AsyncGenericQuery<TEntity>
        : BaseRequest, IAsyncRequest<IEnumerable<TEntity>>,
        IFilterQuery<TEntity>,
        IOrderByQuery<TEntity>,
        IIncludeQuery<TEntity>,
        ITakeQuery
        where TEntity : class
    {
        public const int PAGE_SIZE_MIN = 1;

        public int PageSize { get; private set; }
        public Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> OrderBy { get; private set; }
        public Expression<Func<TEntity, bool>> Predicate { get; private set; }
        public Expression<Func<TEntity, object>>[] IncludeProperties { get; private set; }

        public AsyncGenericQuery(
            int? pageSize = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Expression<Func<TEntity, bool>> predicate = null,
            params Expression<Func<TEntity, object>>[] includeProperties
            )
        {
            PageSize = pageSize.HasValue ? Math.Max(pageSize.Value, PAGE_SIZE_MIN) : 0;
            OrderBy = orderBy;
            Predicate = predicate;
            IncludeProperties = includeProperties;
        }
    }
}
