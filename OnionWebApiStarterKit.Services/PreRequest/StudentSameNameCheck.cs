using Mehdime.Entity;
using OnionWebApiStarterKit.Data.Extensions;
using System;
using System.Threading.Tasks;
using OnionWebApiStarterKit.Core.Services.Command;
using OnionWebApiStarterKit.Core.Services.Decorators;
using OnionWebApiStarterKit.Data;
using OnionWebApiStarterKit.Services.Logging;
using System.Data.Entity;
using OnionWebApiStarterKit.Services.Procedures;

namespace OnionWebApiStarterKit.Services.PreRequest
{
    public class StudentSameNameCheck : IAsyncPreRequestHandler<CreateStudentCommand>
    {
        private readonly IDbContextScopeFactory _dbContextScopeFactory;

        private readonly DoesStudentFirstMidLastNameAlreadyExist _studentExistProcedure;

        public StudentSameNameCheck(IDbContextScopeFactory dbContextScopeFactory, DoesStudentFirstMidLastNameAlreadyExist studentExistProcedure)
        {
            if (dbContextScopeFactory == null)
                throw new ArgumentNullException("dbContextScopeFactory");

            _dbContextScopeFactory = dbContextScopeFactory;

            _studentExistProcedure = studentExistProcedure;
        }

        public async Task Handle(CreateStudentCommand request)
        {
            var logger = LogProvider.For<StudentSameNameCheck>();
            logger.Debug("Start");
            using (var dbContextScope = _dbContextScopeFactory.CreateReadOnly())
            {
                // Gets our context from our context scope
                var dbCtx = dbContextScope.DbContexts.GetByInterface<ISchoolDbContext>();

                // Find if Student with same firstmid and last name already exists
                if (await _studentExistProcedure.HandleAsync<ISchoolDbContext>(request.FirstMidName, request.LastName))
                    throw new InvalidOperationException("A Student with that last and firstmid name already exists");
            }
        }
    }
}
