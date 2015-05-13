using Mehdime.Entity;
using OnionWebApiStarterKit.Data;
using OnionWebApiStarterKit.Data.Extensions;
using System;

namespace OnionWebApiStarterKit.Services.Procedures
{
    //public abstract class BaseProcedure<TDbContext>
    //    where TDbContext : class, IDbContext
    //{
    //    private readonly IAmbientDbContextLocator _ambientDbContextLocator;

    //    protected TDbContext DbContext
    //    {
    //        get
    //        {
    //            var dbContext = _ambientDbContextLocator.GetByInterface<TDbContext>();

    //            if (dbContext == null)
    //                throw new InvalidOperationException("No ambient DbContext found. This means that this repository method has been called outside of the scope of a DbContextScope. A repository must only be accessed within the scope of a DbContextScope, which takes care of creating the DbContext instances that the repositories need and making them available as ambient contexts. This is what ensures that, for any given DbContext-derived type, the same instance is used throughout the duration of a business transaction. To fix this issue, use IDbContextScopeFactory in your top-level business logic service method to create a DbContextScope that wraps the entire business transaction that your service method implements. Then access this repository within that scope. Refer to the comments in the IDbContextScope.cs file for more details.");

    //            return dbContext;
    //        }
    //    }

    //    public BaseProcedure(IAmbientDbContextLocator ambientDbContextLocator)
    //    {
    //        if (ambientDbContextLocator == null) throw new ArgumentNullException("ambientDbContextLocator");
    //        _ambientDbContextLocator = ambientDbContextLocator;
    //    }
    //}

    public abstract class BaseProcedure
    {
        private readonly IAmbientDbContextLocator _ambientDbContextLocator;

        public BaseProcedure(IAmbientDbContextLocator ambientDbContextLocator)
        {
            if (ambientDbContextLocator == null) throw new ArgumentNullException("ambientDbContextLocator");
            _ambientDbContextLocator = ambientDbContextLocator;
        }

        public TDbContext GetDbContext<TDbContext>()
            where TDbContext : class, IDbContext
        {
            var dbContext = _ambientDbContextLocator.GetByInterface<TDbContext>();

            if (dbContext == null)
                throw new InvalidOperationException("No ambient DbContext found. This means that this repository method has been called outside of the scope of a DbContextScope. A repository must only be accessed within the scope of a DbContextScope, which takes care of creating the DbContext instances that the repositories need and making them available as ambient contexts. This is what ensures that, for any given DbContext-derived type, the same instance is used throughout the duration of a business transaction. To fix this issue, use IDbContextScopeFactory in your top-level business logic service method to create a DbContextScope that wraps the entire business transaction that your service method implements. Then access this repository within that scope. Refer to the comments in the IDbContextScope.cs file for more details.");

            return dbContext;
        }
    }
}
