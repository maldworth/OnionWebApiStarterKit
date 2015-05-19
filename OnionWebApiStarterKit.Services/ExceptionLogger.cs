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
    public class ExceptionLogger<TRequest, TResponse>
    : IRequestHandler<TRequest, TResponse>
    where TRequest : BaseRequest, IRequest<TResponse>
    {
        private readonly IRequestHandler<TRequest, TResponse> _inner;

        public ExceptionLogger(
            IRequestHandler<TRequest, TResponse> inner
            )
        {
            _inner = inner;
        }

        public TResponse Handle(TRequest message)
        {
            var log = LogProvider.For<ExceptionLogger<TRequest, TResponse>>();
            log.Debug("Begin");
            try
            {
                return _inner.Handle(message);

            }
            catch (ServiceException e)
            {
                log.ErrorException(e.StackTrace, e);
                e.TrackingId = message.TrackingId.ToString();
                throw;
            }
            catch(Exception e)
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
