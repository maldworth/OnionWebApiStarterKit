using MediatR;
using Mehdime.Entity;
using OnionWebApiStarterKit.Core.Services.Abstracts;
using System;

namespace OnionWebApiStarterKit.Services
{
    public class DbContextScopeBoundary<TRequest, TResponse>
    : IRequestHandler<TRequest, TResponse>
    where TRequest : BaseRequest, IRequest<TResponse>
    {
        private readonly IDbContextScopeFactory _dbContextScopeFactory;
        private readonly IRequestHandler<TRequest, TResponse> _inner;

        public DbContextScopeBoundary(
            IDbContextScopeFactory dbContextScopeFactory,
            IRequestHandler<TRequest, TResponse> inner
            )
        {
            if (dbContextScopeFactory == null)
                throw new ArgumentNullException("dbContextScopeFactory");

            _dbContextScopeFactory = dbContextScopeFactory;
            _inner = inner;
        }

        public TResponse Handle(TRequest message)
        {
            // This will be our parent scope
            using (var dbContextScope = _dbContextScopeFactory.Create())
            {
                var result = _inner.Handle(message);

                // Only the outermost DbContextScope's save changes is respected.
                // For Queries this will not result in any Db Transaction, because there was no change to the EF Context
                dbContextScope.SaveChanges();

                return result;
            }
        }
    }
}
