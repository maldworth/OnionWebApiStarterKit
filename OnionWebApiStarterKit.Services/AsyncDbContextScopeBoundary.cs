using MediatR;
using Mehdime.Entity;
using OnionWebApiStarterKit.Core.Services.Abstracts;
using System;
using System.Threading.Tasks;

namespace OnionWebApiStarterKit.Services
{
    public class AsyncDbContextScopeBoundary<TRequest, TResponse>
    : IAsyncRequestHandler<TRequest, TResponse>
    where TRequest : BaseRequest, IAsyncRequest<TResponse>
    {
        private readonly IDbContextScopeFactory _dbContextScopeFactory;
        private readonly IAsyncRequestHandler<TRequest, TResponse> _inner;

        public AsyncDbContextScopeBoundary(
            IDbContextScopeFactory dbContextScopeFactory,
            IAsyncRequestHandler<TRequest, TResponse> inner
            )
        {
            if (dbContextScopeFactory == null)
                throw new ArgumentNullException("dbContextScopeFactory");

            _dbContextScopeFactory = dbContextScopeFactory;
            _inner = inner;
        }

        public async Task<TResponse> Handle(TRequest message)
        {
            // This will be our parent scope
            using (var dbContextScope = _dbContextScopeFactory.Create())
            {
                var result = await _inner.Handle(message);

                // Only the outermost DbContextScope's save changes is respected.
                // For Queries this will not result in any Db Transaction, because there was no change to the EF Context
                await dbContextScope.SaveChangesAsync();

                return result;
            }
        }
    }
}
