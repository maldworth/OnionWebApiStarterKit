using MediatR;
using OnionWebApiStarterKit.Core.Services.Abstracts;
using OnionWebApiStarterKit.Core.Services.Decorators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionWebApiStarterKit.Services
{
    public class AsyncLoggingHandler<TRequest, TResponse> : IAsyncRequestHandler<TRequest, TResponse>
        where TRequest : BaseRequest, IAsyncRequest<TResponse>
    {
        private readonly IAsyncRequestHandler<TRequest, TResponse> _inner;

        public AsyncLoggingHandler(IAsyncRequestHandler<TRequest, TResponse> inner)
        {
            _inner = inner;
        }

        public async Task<TResponse> Handle(TRequest request)
        {
            var baseRequest = (BaseRequest)request;

            log4net.LogicalThreadContext.Properties["TrackingId"] = baseRequest.TrackingId.ToString();

            return await _inner.Handle(request);
        }
    }
}
