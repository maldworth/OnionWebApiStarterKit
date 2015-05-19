using OnionWebApiStarterKit.Core.DomainModels;
using OnionWebApiStarterKit.Core.Dto;
using FluentValidation.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using OnionWebApiStarterKit.MyWebApi.ViewModels;
using OnionWebApiStarterKit.MyWebApi.Filters;

namespace OnionWebApiStarterKit.MyWebApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            var validatorFactory = new OnionWebApiStarterKit.MyWebApi.App_Start.FluentValidatorFactory();
            FluentValidationModelValidatorProvider.Configure(config, provider => provider.ValidatorFactory = validatorFactory);

            // Action Filters
            config.Filters.Add(new FluentValidationActionFilter());

            // Exception Filter
            config.Filters.Add(new ServiceExceptionFilterAttribute());

            config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Never;

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
