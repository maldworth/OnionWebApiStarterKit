using Mehdime.Entity;
using OnionWebApiStarterKit.Services.BaseQuery;
using OnionWebApiStarterKit.Data;

namespace OnionWebApiStarterKit.Services.Query
{
    public class PaginateQueryHandler<TEntity> : BasePaginateQueryHandler<TEntity, ISchoolDbContext>
        where TEntity : class
    {
        public PaginateQueryHandler(IDbContextScopeFactory dbContextScopeFactory)
            : base(dbContextScopeFactory) { }
    }
}
