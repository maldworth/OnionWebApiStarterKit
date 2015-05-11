using Mehdime.Entity;
using OnionWebApiStarterKit.Data.Extensions;
using System;
using System.Threading.Tasks;
using OnionWebApiStarterKit.Core.Services.Command;
using OnionWebApiStarterKit.Core.Services.Decorators;
using OnionWebApiStarterKit.Data;
using OnionWebApiStarterKit.Services.Logging;
using System.Data.Entity;

namespace OnionWebApiStarterKit.Services.PreRequest
{
    public class StudentSameNameCheck : IAsyncPreRequestHandler<CreateStudentCommand>
    {
        private readonly IDbContextScopeFactory _dbContextScopeFactory;

        public StudentSameNameCheck(IDbContextScopeFactory dbContextScopeFactory)
        {
            if (dbContextScopeFactory == null)
                throw new ArgumentNullException("dbContextScopeFactory");

            _dbContextScopeFactory = dbContextScopeFactory;
        }

        public async Task Handle(CreateStudentCommand request)
        {
            var logger = LogProvider.For<StudentSameNameCheck>();
            logger.Debug("Testzz");
            using (var dbContextScope = _dbContextScopeFactory.CreateReadOnly())
            {
                // Gets our context from our context scope
                var dbCtx = dbContextScope.DbContexts.GetByInterface<ISchoolDbContext>();

                // Find if Student with same firstmid and last name already exists
                var count = await dbCtx.Students.CountAsync(x => x.FirstMidName == request.FirstMidName && x.LastName == request.LastName);

                if (count > 0)
                    throw new InvalidOperationException("A Student with that last and firstmid name already exists");
            }
        }
    }
}
