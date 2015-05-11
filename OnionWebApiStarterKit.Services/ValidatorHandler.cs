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
    public class ValidatorHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
        where TRequest : BaseRequest, IRequest<TResponse>
    {
        private readonly IRequestHandler<TRequest, TResponse> _inner;
        private readonly IValidator<TRequest>[] _validators;
        private readonly ILog _log;

        public ValidatorHandler(
            IRequestHandler<TRequest, TResponse> inner,
            IValidator<TRequest>[] validators)
        {
            _inner = inner;
            _validators = validators;
            _log = LogProvider.For<ValidatorHandler<TRequest, TResponse>>();
        }

        public TResponse Handle(TRequest request)
        {
            _log.Trace(() => "Start");
            //_log.Trace(request.ToString());

            var context = new ValidationContext(request);

            var failures = _validators
                .Select(v => v.Validate(context))
                .SelectMany(result => result.Errors)
                .Where(f => f != null)
                .ToList();

            if (failures.Any())
                throw new ValidationException(failures);

            return _inner.Handle(request);
        }
    }
}
