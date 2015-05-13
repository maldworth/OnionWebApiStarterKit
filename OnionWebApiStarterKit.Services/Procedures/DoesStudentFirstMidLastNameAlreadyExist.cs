using OnionWebApiStarterKit.Data;
using System.Linq;
using System.Data.Entity;
using System.Threading.Tasks;
using Mehdime.Entity;
using OnionWebApiStarterKit.Core.DomainModels;
using OnionWebApiStarterKit.Services.Abstracts;
using OnionWebApiStarterKit.Core.Services;

namespace OnionWebApiStarterKit.Services.Procedures
{
    // A very simplified example, but to have reusable procedures, it's best to break the logic out into a BaseProcedure
    public class DoesStudentFirstMidLastNameAlreadyExist
        : BaseAmbientDbContextScope, IProcedure
    {
        public DoesStudentFirstMidLastNameAlreadyExist(IAmbientDbContextLocator ambientDbContextLocator)
            : base(ambientDbContextLocator)
        { }

        public bool Handle<TDbContext>(string firstMidName, string lastName)
            where TDbContext : class, IDbContext
        {
            var count = GetDbContext<TDbContext>().Set<Student>().Count(x => x.FirstMidName == firstMidName && x.LastName == lastName);

            return count > 0;
        }

        public async Task<bool> HandleAsync<TDbContext>(string firstMidName, string lastName)
            where TDbContext : class, IDbContext
        {
            var count = await GetDbContext<TDbContext>().Set<Student>().CountAsync(x => x.FirstMidName == firstMidName && x.LastName == lastName);

            return count > 0;
        }
    }
}
