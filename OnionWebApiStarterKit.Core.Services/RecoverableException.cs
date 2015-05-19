using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace OnionWebApiStarterKit.Core.Services
{
    public class RecoverableException : ServiceException
    {
        public RecoverableException()
        { }

        public RecoverableException(string message)
            : base(message)
        { }

        public RecoverableException(string message, Exception exception)
            : base(message, exception)
        {
        }

        public RecoverableException(string message, string trackingId)
            : base(message, trackingId)
        {
        }

        public RecoverableException(string message, string trackingId, Exception exception)
            : base(message, trackingId, exception)
        {
        }

        protected RecoverableException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
