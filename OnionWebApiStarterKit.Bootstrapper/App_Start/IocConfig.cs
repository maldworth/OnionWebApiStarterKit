using OnionWebApiStarterKit.Bootstrapper;
using OnionWebApiStarterKit.Data;
using OnionWebApiStarterKit.Services;
using Autofac;
using Autofac.Integration.WebApi;
using Mehdime.Entity;
using System.Web.Compilation;
using System.Linq;
using OnionWebApiStarterKit.MyWebApi;
using System.Web.Http;
using System.Data.Entity;
using FluentValidation;
using System.Reflection;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(IocConfig), "RegisterDependencies")]

namespace OnionWebApiStarterKit.Bootstrapper
{
    public class IocConfig
    {
        public static void RegisterDependencies()
        {
            DbContextScopeExtensionConfig.Setup();

            var builder = new ContainerBuilder();

            // Get HttpConfiguration
            var config = GlobalConfiguration.Configuration;

            builder.RegisterApiControllers(typeof(WebApiApplication).Assembly);

            // This is used by odata endpoint, not the webapi endpoint
            builder.RegisterType<SchoolDbContext>().As<ISchoolDbContext>().InstancePerRequest();
            
            builder.RegisterType<DbContextScopeFactory>().As<IDbContextScopeFactory>().SingleInstance();
            
            // Registers our IMediator (abstraction for observer pattern, which lets us use CQRS)
            builder.RegisterModule(new MediatorModule(Assembly.Load("OnionWebApiStarterKit.Services")));

            // Registers our Fluent Validations that we use on our Models
            builder.RegisterModule(new FluentValidationModule(Assembly.Load("OnionWebApiStarterKit.MyWebApi"), Assembly.Load("OnionWebApiStarterKit.Services")));

            // Registers our AutoMapper Profiles
            builder.RegisterModule(new AutoMapperModule(Assembly.Load("OnionWebApiStarterKit.MyWebApi"), Assembly.Load("OnionWebApiStarterKit.Services")));

            var container = builder.Build();

            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}
