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
    public class LoggingHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
        where TRequest : BaseRequest, IRequest<TResponse>
    {
        private readonly IRequestHandler<TRequest, TResponse> _inner;

        public LoggingHandler(IRequestHandler<TRequest, TResponse> inner)
        {
            _inner = inner;
        }

        public TResponse Handle(TRequest request)
        {
            var baseRequest = (BaseRequest)request;

            log4net.LogicalThreadContext.Properties["TrackingId"] = baseRequest.TrackingId.ToString();

            return _inner.Handle(request);
        }
    }
}
