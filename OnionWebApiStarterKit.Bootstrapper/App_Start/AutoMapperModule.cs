using Autofac;
using AutoMapper;

namespace OnionWebApiStarterKit.Bootstrapper
{
    public class AutoMapperModule : Module
    {
        private readonly System.Reflection.Assembly[] _assembliesToScan;

        public AutoMapperModule(params System.Reflection.Assembly[] assembliesToScan)
            : base()
        {
            _assembliesToScan = assembliesToScan;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(_assembliesToScan)
                .Where(t => t.BaseType == typeof(Profile))
                .As<Profile>();
        }
    }
}
