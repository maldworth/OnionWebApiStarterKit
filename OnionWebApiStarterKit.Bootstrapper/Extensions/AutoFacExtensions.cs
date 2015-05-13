using System;
using System.Linq;
using Autofac;
using Autofac.Builder;
using Autofac.Core;
using Autofac.Features.Scanning;
using System.Reflection;

namespace OnionWebApiStarterKit.Bootstrapper.Extensions
{
    // From here
    // http://stackoverflow.com/questions/8140714/autofac-decorating-open-generics-registered-using-assembly-scanning
    public static class AutoFacExtensions
    {
        public static IRegistrationBuilder<TLimit, TScanningActivatorData, TRegistrationStyle>
            AsClosedTypesOf<TLimit, TScanningActivatorData, TRegistrationStyle>(
                this IRegistrationBuilder<TLimit, TScanningActivatorData, TRegistrationStyle> registration,
                Type openGenericServiceType,
                object key)
            where TScanningActivatorData : ScanningActivatorData
        {
            if (openGenericServiceType == null) throw new ArgumentNullException("openGenericServiceType");

            return registration.As(t =>
                new[] { t }
                .Concat(t.GetInterfaces())
                .Where(i => i.IsClosedTypeOf(openGenericServiceType))
                .Select(i => new KeyedService(key, i)));
        }


        // Adapted From here
        // https://stackoverflow.com/a/26018954

        /// <summary>
        /// Scans the supplied assemblies for the marker and registers them agains the closed types of the open generic handler. Then registers the generic decorator for each.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="assembliesToScan"></param>
        /// <param name="markerType"></param>
        /// <param name="handlerTypeOpenGeneric"></param>
        /// <param name="decorators"></param>
        public static void ScanRegisterAndDecorate(
            this ContainerBuilder builder,
            Assembly[] assembliesToScan,
            Type markerType,
            Type handlerTypeOpenGeneric,
            params Type[] decorators)
        {
            // First register our markerTypes
            builder.RegisterAssemblyTypes(assembliesToScan)
                .Where(t => t.GetInterfaces().Any(i => i == markerType))
                .AsClosedTypesOf(handlerTypeOpenGeneric, markerType.Name)
                .SingleInstance();

            // Now Decorate
            for (int i = 0; i < decorators.Length; i++)
            {
                RegisterGenericDecorator(
                    builder,
                    decorators[i],
                    handlerTypeOpenGeneric,
                    i == 0 ? markerType : decorators[i - 1],
                    i != decorators.Length - 1);
            }
        }

        private static void RegisterGenericDecorator(
            ContainerBuilder builder,
            Type decoratorType,
            Type decoratedServiceType,
            Type fromKeyType,
            bool hasKey)
        {
            var result = builder.RegisterGenericDecorator(
               decoratorType,
               decoratedServiceType,
               fromKeyType.Name);

            if (hasKey)
            {
                result.Keyed(decoratorType.Name, decoratedServiceType);
            }
        }
    }
}