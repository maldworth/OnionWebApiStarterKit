using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionWebApiStarterKit.Core.Services.Abstracts
{
    public abstract class BaseRequest
    {
        private readonly Guid _trackingId;

        public Guid TrackingId { get { return _trackingId; } }

        public BaseRequest()
        {
            _trackingId = Guid.NewGuid();
        }
    }
}
