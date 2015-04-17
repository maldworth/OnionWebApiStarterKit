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
    public class MediatorPipeline<TRequest, TResponse>
    : IRequestHandler<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    {
        private readonly IDbContextScopeFactory _dbContextScopeFactory;
        private readonly IRequestHandler<TRequest, TResponse> _inner;
        private readonly IPreRequestHandler<TRequest>[] _preRequestHandlers;

        public MediatorPipeline(
            IDbContextScopeFactory dbContextScopeFactory,
            IRequestHandler<TRequest, TResponse> inner,
            IPreRequestHandler<TRequest>[] preRequestHandlers
            )
        {
            if (dbContextScopeFactory == null)
                throw new ArgumentNullException("dbContextScopeFactory");

            _dbContextScopeFactory = dbContextScopeFactory;
            _inner = inner;
            _preRequestHandlers = preRequestHandlers;
        }

        public TResponse Handle(TRequest message)
        {
            // This will be our parent scope
            using (var dbContextScope = _dbContextScopeFactory.Create())
            {
                foreach (var preRequestHandler in _preRequestHandlers)
                {
                    preRequestHandler.Handle(message);
                }

                var result = _inner.Handle(message);

                // Only the outermost DbContextScope's save changes is respected.
                // For Queries this will not result in any Db Transaction, because there was no change to the EF Context
                dbContextScope.SaveChanges();

                return result;
            }
        }
    }
}
