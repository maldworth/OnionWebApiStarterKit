using Mehdime.Entity;
using OnionWebApiStarterKit.Data;

namespace OnionWebApiStarterKit.Services.Query
{
    public class AsyncAutoMapperQueryHandler<TSrcEntity, TDestModel> : BaseAsyncAutoMapperQueryHandler<TSrcEntity, TDestModel, ISchoolDbContext>
        where TSrcEntity : class
    {
        public AsyncAutoMapperQueryHandler(IDbContextScopeFactory dbContextScopeFactory)
            : base(dbContextScopeFactory) { }
    }
}
