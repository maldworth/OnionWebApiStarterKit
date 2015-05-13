using Mehdime.Entity;
using OnionWebApiStarterKit.Services.BaseQuery;
using OnionWebApiStarterKit.Data;
using OnionWebApiStarterKit.Core.Services;

namespace OnionWebApiStarterKit.Services.Query
{
    public class AsyncGenericQueryHandler<TEntity> : BaseAsyncGenericQueryHandler<TEntity, ISchoolDbContext>
        where TEntity : class
    {
        public AsyncGenericQueryHandler(IDbContextScopeFactory dbContextScopeFactory)
            : base(dbContextScopeFactory) { }
    }
}
