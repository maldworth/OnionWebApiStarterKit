using MediatR;
using Mehdime.Entity;
using OnionWebApiStarterKit.Core.Services;
using OnionWebApiStarterKit.Core.Services.Abstracts;
using OnionWebApiStarterKit.Core.Services.Decorators;
using OnionWebApiStarterKit.Services.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionWebApiStarterKit.Services
{
    public class ExceptionLoggerAsync<TRequest, TResponse>
    : IAsyncRequestHandler<TRequest, TResponse>
    where TRequest : BaseRequest, IAsyncRequest<TResponse>
    {
        private readonly IAsyncRequestHandler<TRequest, TResponse> _inner;

        public ExceptionLoggerAsync(
            IAsyncRequestHandler<TRequest, TResponse> inner
            )
        {
            _inner = inner;
        }

        public async Task<TResponse> Handle(TRequest message)
        {
            var log = LogProvider.For<ExceptionLoggerAsync<TRequest, TResponse>>();
            log.Debug("Begin");
            try
            {
                return await _inner.Handle(message);
            }
            catch (RecoverableException e)
            {
                log.ErrorException(e.StackTrace, e);
                e.TrackingId = message.TrackingId.ToString();
                throw;
            }
            catch (Exception e)
            {
                log.ErrorException(e.StackTrace, e);
                throw new ServiceException(e.Message, message.TrackingId.ToString(), e);
            }
            finally
            {
                log.Debug("End");
            }
        }
    }
}
