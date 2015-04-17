using Mehdime.Entity;
using OnionWebApiStarterKit.Services.BaseQuery;
using OnionWebApiStarterKit.Data;

namespace OnionWebApiStarterKit.Services.Query
{
    public class GenericQueryHandler<TEntity> : BaseGenericQueryHandler<TEntity, ISchoolDbContext>
        where TEntity : class
    {
        public GenericQueryHandler(IDbContextScopeFactory dbContextScopeFactory)
            : base(dbContextScopeFactory) { }
    }
}
