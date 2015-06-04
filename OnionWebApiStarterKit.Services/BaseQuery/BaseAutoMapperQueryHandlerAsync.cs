using OnionWebApiStarterKit.Core.Services.Extensions;
using OnionWebApiStarterKit.Data.Extensions;
using MediatR;
using Mehdime.Entity;
using System;
using System.Linq;
using System.Data.Entity;
using System.Collections.Generic;
using AutoMapper.QueryableExtensions;
using OnionWebApiStarterKit.Core.Services.Query;
using OnionWebApiStarterKit.Data;
using System.Threading.Tasks;

namespace OnionWebApiStarterKit.Services.Query
{
    public abstract class BaseAsyncAutoMapperQueryHandler<TSrcEntity, TDestModel, TDbContext>
        : IAsyncRequestHandler<AsyncAutoMapperQuery<TSrcEntity, TDestModel>, IEnumerable<TDestModel>>
        where TSrcEntity : class
        where TDbContext : class, IDbContext
    {
        private readonly IDbContextScopeFactory _dbContextScopeFactory;

        public BaseAsyncAutoMapperQueryHandler(IDbContextScopeFactory dbContextScopeFactory)
        {
            _dbContextScopeFactory = dbContextScopeFactory;
        }

        public async Task<IEnumerable<TDestModel>> Handle(AsyncAutoMapperQuery<TSrcEntity, TDestModel> args)
        {
            using (var dbContextScope = _dbContextScopeFactory.CreateReadOnly())
            {
                // Gets our context from our context scope
                var dbCtx = dbContextScope.DbContexts.GetByInterface<TDbContext>();

                IQueryable<TSrcEntity> srcEntities = dbCtx.Set<TSrcEntity>();

                srcEntities = srcEntities.Where(args);
                IQueryable<TDestModel> destEntities = srcEntities.Project().To<TDestModel>();
                destEntities = destEntities.OrderBy(args);
                if (args.PageSize != 0)
                    destEntities = destEntities.Take(args.PageSize);
                return await destEntities.ToListAsync();
            }
        }
    }
}
