using MediatR;
using Mehdime.Entity;
using OnionWebApiStarterKit.Core.Services.Decorators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionWebApiStarterKit.Services
{
    public class AsyncMediatorPipeline<TRequest, TResponse>
    : IAsyncRequestHandler<TRequest, TResponse>
    where TRequest : IAsyncRequest<TResponse>
    {
        private readonly IDbContextScopeFactory _dbContextScopeFactory;
        private readonly IAsyncRequestHandler<TRequest, TResponse> _inner;
        private readonly IAsyncPreRequestHandler<TRequest>[] _preRequestHandlers;

        public AsyncMediatorPipeline(
            IDbContextScopeFactory dbContextScopeFactory,
            IAsyncRequestHandler<TRequest, TResponse> inner,
            IAsyncPreRequestHandler<TRequest>[] preRequestHandlers
            )
        {
            if (dbContextScopeFactory == null)
                throw new ArgumentNullException("dbContextScopeFactory");

            _dbContextScopeFactory = dbContextScopeFactory;
            _inner = inner;
            _preRequestHandlers = preRequestHandlers;
        }

        public async Task<TResponse> Handle(TRequest message)
        {
            // This will be our parent scope
            using (var dbContextScope = _dbContextScopeFactory.Create())
            {
                foreach (var preRequestHandler in _preRequestHandlers)
                {
                    await preRequestHandler.Handle(message);
                }

                var result = await _inner.Handle(message);

                // Only the outermost DbContextScope's save changes is respected.
                // For Queries this will not result in any Db Transaction, because there was no change to the EF Context
                await dbContextScope.SaveChangesAsync();

                return result;
            }
        }
    }
}
