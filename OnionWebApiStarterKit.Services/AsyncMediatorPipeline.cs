using MediatR;
using Mehdime.Entity;
using OnionWebApiStarterKit.Core.Services.Abstracts;
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
    where TRequest : BaseRequest, IAsyncRequest<TResponse>
    {
        private readonly IAsyncRequestHandler<TRequest, TResponse> _inner;
        private readonly IAsyncPreRequestHandler<TRequest>[] _preRequestHandlers;

        public AsyncMediatorPipeline(
            IAsyncRequestHandler<TRequest, TResponse> inner,
            IAsyncPreRequestHandler<TRequest>[] preRequestHandlers
            )
        {
            _inner = inner;
            _preRequestHandlers = preRequestHandlers;
        }

        public async Task<TResponse> Handle(TRequest message)
        {
            foreach (var preRequestHandler in _preRequestHandlers)
            {
                await preRequestHandler.Handle(message);
            }

            return await _inner.Handle(message);
        }
    }
}
