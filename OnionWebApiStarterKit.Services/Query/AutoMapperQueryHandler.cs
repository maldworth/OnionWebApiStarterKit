using Mehdime.Entity;
using OnionWebApiStarterKit.Data;

namespace OnionWebApiStarterKit.Services.Query
{
    public class AutoMapperQueryHandler<TSrcEntity, TDestModel> : BaseAutoMapperQueryHandler<TSrcEntity, TDestModel, ISchoolDbContext>
        where TSrcEntity : class
    {
        public AutoMapperQueryHandler(IDbContextScopeFactory dbContextScopeFactory)
            : base(dbContextScopeFactory) { }
    }
}
