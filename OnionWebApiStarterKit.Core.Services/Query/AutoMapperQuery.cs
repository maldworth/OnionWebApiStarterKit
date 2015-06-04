using MediatR;
using OnionWebApiStarterKit.Core.Services;
using OnionWebApiStarterKit.Core.Services.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace OnionWebApiStarterKit.Core.Services.Query
{
    public class AutoMapperQuery<TSrcEntity, TDestModel>
        : BaseRequest, IRequest<IEnumerable<TDestModel>>,
        IFilterQuery<TSrcEntity>,
        IOrderByQuery<TDestModel>,
        ITakeQuery
        where TSrcEntity : class
    {
        public const int PAGE_SIZE_MIN = 1;

        public int PageSize { get; private set; }
        public Func<IQueryable<TDestModel>, IOrderedQueryable<TDestModel>> OrderBy { get; private set; }
        public Expression<Func<TSrcEntity, bool>> Predicate { get; private set; }

        public AutoMapperQuery(
            int? pageSize = null,
            Func<IQueryable<TDestModel>, IOrderedQueryable<TDestModel>> orderBy = null,
            Expression<Func<TSrcEntity, bool>> predicate = null
            )
        {
            PageSize = pageSize.HasValue ? Math.Max(pageSize.Value, PAGE_SIZE_MIN) : 0;
            OrderBy = orderBy;
            Predicate = predicate;
        }
    }
}
