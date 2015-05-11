using MediatR;
using OnionWebApiStarterKit.Core.Services;
using OnionWebApiStarterKit.Core.Services.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace OnionWebApiStarterKit.Core.Services.Query
{
    public class AsyncAutoMapperQuery<TSrcEntity, TDestModel>
        : BaseRequest,
        IAsyncRequest<IEnumerable<TDestModel>>,
        IFilterQuery<TSrcEntity>,
        IOrderByQuery<TDestModel>,
        ITakeQuery
        where TSrcEntity : class
    {
        public const int PAGE_SIZE_MIN = 1;
        public const int PAGE_SIZE_MAX = 100;

        public int PageSize { get; private set; }
        public Func<IQueryable<TDestModel>, IOrderedQueryable<TDestModel>> OrderBy { get; private set; }
        public Expression<Func<TSrcEntity, bool>> Predicate { get; private set; }

        public AsyncAutoMapperQuery(
            int pageSize,
            Func<IQueryable<TDestModel>, IOrderedQueryable<TDestModel>> orderBy = null,
            Expression<Func<TSrcEntity, bool>> predicate = null
            )
        {
            PageSize = Math.Min(Math.Max(pageSize, PAGE_SIZE_MIN), PAGE_SIZE_MAX);
            OrderBy = orderBy;
            Predicate = predicate;
        }
    }
}
