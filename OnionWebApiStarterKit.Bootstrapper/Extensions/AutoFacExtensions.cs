using System;
using System.Linq;
using Autofac;
using Autofac.Builder;
using Autofac.Core;
using Autofac.Features.Scanning;

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
    }
}