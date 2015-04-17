using Autofac;
using FluentValidation;

namespace OnionWebApiStarterKit.Bootstrapper
{
    public class FluentValidationModule : Module
    {
        private readonly System.Reflection.Assembly[] _assembliesToScan;

        public FluentValidationModule(params System.Reflection.Assembly[] assembliesToScan)
            : base()
        {
            _assembliesToScan = assembliesToScan;
        }

        protected override void Load(ContainerBuilder builder)
        {
            // Scans the Web Assembly for all closed types that implement IValidator<>, which should be all our AbstractValidators
            builder.RegisterAssemblyTypes(_assembliesToScan)
                .AsClosedTypesOf(typeof(IValidator<>))
                .AsImplementedInterfaces();
        }
    }
}
