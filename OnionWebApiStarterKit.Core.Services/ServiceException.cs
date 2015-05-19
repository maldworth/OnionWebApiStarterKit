using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace OnionWebApiStarterKit.Core.Services
{
    [Serializable]
    public class ServiceException : Exception
    {
        private string _trackingId;

        public ServiceException()
        { }

        public ServiceException(string message)
            : base(message)
        { }

        public ServiceException(string message, Exception exception)
            : base(message, exception)
        {
        }

        public ServiceException(string message, string trackingId)
            : base(message)
        {
            _trackingId = trackingId;
        }

        public ServiceException(string message, string trackingId, Exception exception)
            : base(message, exception)
        {
            _trackingId = trackingId;
        }

        [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
        protected ServiceException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            _trackingId = info.GetString("TrackingId");
        }

        public string TrackingId
        {
            get { return _trackingId; }
            set
            {
                if (string.IsNullOrWhiteSpace(_trackingId))
                    _trackingId = value;
                else
                    throw new ArgumentException("Only assignable once.");
            }
        }

        [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
            {
                throw new ArgumentNullException("info");
            }

            info.AddValue("TrackingId", TrackingId);

            base.GetObjectData(info, context);
        }
    }
}
