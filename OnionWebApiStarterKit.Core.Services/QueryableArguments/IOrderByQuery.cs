using System;
using System.Linq;
using System.Linq.Expressions;

namespace OnionWebApiStarterKit.Core.Services
{
    public interface IOrderByQuery<TEntity>
    {
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> OrderBy { get; }
    }
}
