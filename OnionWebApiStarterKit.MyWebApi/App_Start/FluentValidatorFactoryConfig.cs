using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Dependencies;

namespace OnionWebApiStarterKit.MyWebApi.App_Start
{
    public class FluentValidatorFactory : ValidatorFactoryBase
    {
        public override IValidator CreateInstance(Type validatorType)
        {
            return GlobalConfiguration.Configuration.DependencyResolver.GetService(validatorType) as IValidator;
        }
    }
}