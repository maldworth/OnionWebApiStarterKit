using OnionWebApiStarterKit.Core.Services;
using System.ComponentModel;
using System.Linq;

namespace OnionWebApiStarterKit.Core.Services.Extensions
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class QueryableExtensions
    {
        public static IQueryable<T> Paginate<T>(this IQueryable<T> query, IPaginateQuery<T> args)
        {
            var entities = query.Skip((args.PageIndex - 1) * args.PageSize).Take(args.PageSize);
            return entities;
        }

        public static IQueryable<T> Where<T>(this IQueryable<T> query, IFilterQuery<T> args)
        {
            query = (args.Predicate != null) ? query.Where(args.Predicate) : query;

            return query;
        }

        public static IQueryable<T> OrderBy<T>(this IQueryable<T> query, IOrderByQuery<T> args)
        {
            query = (args.OrderBy != null)
                ? args.OrderBy(query)
                : query;

            return query;
        }
    }
}
