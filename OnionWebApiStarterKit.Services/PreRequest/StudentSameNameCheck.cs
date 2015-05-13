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
using OnionWebApiStarterKit.Services.Abstracts;

namespace OnionWebApiStarterKit.Services.PreRequest
{
    public class StudentSameNameCheck
        : IAsyncPreRequestHandler<CreateStudentCommand>
    {
        private readonly DoesStudentFirstMidLastNameAlreadyExist _studentExistProcedure;

        public StudentSameNameCheck(DoesStudentFirstMidLastNameAlreadyExist studentExistProcedure)
        {
            _studentExistProcedure = studentExistProcedure;
        }

        public async Task Handle(CreateStudentCommand request)
        {
            var logger = LogProvider.For<StudentSameNameCheck>();
            logger.Debug("Start");
            
            // Find if Student with same firstmid and last name already exists
            if (await _studentExistProcedure.HandleAsync<ISchoolDbContext>(request.FirstMidName, request.LastName))
                throw new InvalidOperationException("A Student with that last and firstmid name already exists");
        }
    }
}
