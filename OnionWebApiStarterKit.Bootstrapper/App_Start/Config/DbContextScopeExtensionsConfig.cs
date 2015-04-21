using OnionWebApiStarterKit.Data;
using OnionWebApiStarterKit.Data.Extensions;

namespace OnionWebApiStarterKit.Bootstrapper
{
    public static class DbContextScopeExtensionConfig
    {
        public static void Setup()
        {
            DbContextScopeExtensions.GetDbContextFromCollection = (collection, type) =>
            {
                if(type == typeof(ISchoolDbContext))
                    return collection.Get<SchoolDbContext>();
                return null;
            };

            DbContextScopeExtensions.GetDbContextFromLocator = (locator, type) =>
            {
                if (type == typeof(ISchoolDbContext))
                    return locator.Get<SchoolDbContext>();
                return null;

            };
        }
    }
}
