using OnionWebApiStarterKit.Core.DomainModels;
using OnionWebApiStarterKit.Core.Dto;
using AutoMapper;
using System.Web.Http;

namespace OnionWebApiStarterKit.MyWebApi
{
    public class AutoMapperConfig
    {
        public static void Initialize()
        {
            var profiles = GlobalConfiguration.Configuration.DependencyResolver.GetServices(typeof(Profile));

            Mapper.Initialize(cfg =>
            {
                foreach (Profile profile in profiles)
                    cfg.AddProfile(profile);
            });
        }
    }
}