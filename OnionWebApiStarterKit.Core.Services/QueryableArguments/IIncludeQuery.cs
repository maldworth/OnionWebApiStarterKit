using System;
using System.Linq.Expressions;

namespace OnionWebApiStarterKit.Core.Services
{
    public interface IIncludeQuery<TEntity>
    {
        Expression<Func<TEntity, object>>[] IncludeProperties { get; }
    }
}
