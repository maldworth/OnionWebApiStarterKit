using MediatR;
using Mehdime.Entity;
using OnionWebApiStarterKit.Core.Services.Extensions;
using OnionWebApiStarterKit.Data.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Entity;
using OnionWebApiStarterKit.Core.Services.Query;
using OnionWebApiStarterKit.Data;

namespace OnionWebApiStarterKit.Services.BaseQuery
{
    public abstract class BaseAsyncGenericQueryHandler<TEntity, TDbContext> : IAsyncRequestHandler<AsyncGenericQuery<TEntity>, IEnumerable<TEntity>>
        where TEntity : class
        where TDbContext : class, IDbContext
    {
        private readonly IDbContextScopeFactory _dbContextScopeFactory;

        public BaseAsyncGenericQueryHandler(IDbContextScopeFactory dbContextScopeFactory)
        {
            if (dbContextScopeFactory == null)
                throw new ArgumentNullException("dbContextScopeFactory");

            _dbContextScopeFactory = dbContextScopeFactory;
        }

        public async Task<IEnumerable<TEntity>> Handle(AsyncGenericQuery<TEntity> args)
        {
            using (var dbContextScope = _dbContextScopeFactory.CreateReadOnly())
            {
                IDbContext dbCtx = dbContextScope.DbContexts.GetByInterface<TDbContext>();

                ((DbContext)dbCtx).Configuration.ProxyCreationEnabled = false;

                IQueryable<TEntity> entities = dbCtx.Set<TEntity>();

                entities = entities.Include(args);
                entities = entities.Where(args);
                entities = entities.OrderBy(args);

                // Depending on your needs, you may not want to have .Take be mandatory
                return await entities.Take(args.PageSize).ToListAsync();
            }
        }
    }
}
