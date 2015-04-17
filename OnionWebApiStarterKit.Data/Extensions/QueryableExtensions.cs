using OnionWebApiStarterKit.Core.Services;
using System.ComponentModel;
using System.Linq;
using System.Data.Entity;

namespace OnionWebApiStarterKit.Data.Extensions
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class QueryableExtensions
    {
        public static IQueryable<T> Include<T>(this IQueryable<T> query, IIncludeQuery<T> args)
            where T : class
        {
            if (args.IncludeProperties != null)
                foreach (var include in args.IncludeProperties)
                {
                    query = query.Include(include);

                }

            return query;
        }
    }
}
