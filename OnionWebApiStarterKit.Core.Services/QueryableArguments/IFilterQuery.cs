using System;
using System.Linq.Expressions;

namespace OnionWebApiStarterKit.Core.Services
{
    public interface IFilterQuery<TEntity>
    {
        Expression<Func<TEntity, bool>> Predicate { get; }
    }
}
