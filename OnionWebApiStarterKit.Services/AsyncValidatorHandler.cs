using FluentValidation;
using MediatR;
using OnionWebApiStarterKit.Core.Services.Abstracts;
using OnionWebApiStarterKit.Services.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionWebApiStarterKit.Services
{
    public class AsyncValidatorHandler<TRequest, TResponse> : IAsyncRequestHandler<TRequest, TResponse>
        where TRequest : BaseRequest, IAsyncRequest<TResponse>
    {
        private readonly IAsyncRequestHandler<TRequest, TResponse> _inner;
        private readonly IValidator<TRequest>[] _validators;
        private readonly ILog _log;

        public AsyncValidatorHandler(
            IAsyncRequestHandler<TRequest, TResponse> inner,
            IValidator<TRequest>[] validators)
        {
            _inner = inner;
            _validators = validators;
            _log = LogProvider.For<AsyncValidatorHandler<TRequest, TResponse>>();
        }

        public async Task<TResponse> Handle(TRequest request)
        {
            _log.Trace(() => "Start");

            var context = new ValidationContext(request);

            var failures = _validators
                .Select(v => v.Validate(context))
                .SelectMany(result => result.Errors)
                .Where(f => f != null)
                .ToList();

            if (failures.Any())
                throw new ValidationException(failures);

            return await _inner.Handle(request);
        }
    }
}
