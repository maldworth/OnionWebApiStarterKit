using OnionWebApiStarterKit.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;
using System.Web.Http.Filters;
using System.Net;

namespace OnionWebApiStarterKit.MyWebApi.Filters
{
    public class ServiceExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            var request = actionExecutedContext.ActionContext.Request;

            var exception = actionExecutedContext.Exception as ServiceException;
            if(exception != null)
            {
                var response = new{
                    Message = exception.Message,
                    TrackingId = exception.TrackingId
                };
                actionExecutedContext.Response = request.CreateResponse(HttpStatusCode.InternalServerError, response);
            }
        }
    }
}