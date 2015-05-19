using OnionWebApiStarterKit.Core.DomainModels;
using OnionWebApiStarterKit.Data.Extensions;
using MediatR;
using Mehdime.Entity;
using System;
using System.Linq;
using System.Data.Entity;
using System.Collections.Generic;
using AutoMapper.QueryableExtensions;
using OnionWebApiStarterKit.Core.Services.Command;
using OnionWebApiStarterKit.Data;
using System.Threading.Tasks;
using OnionWebApiStarterKit.Services.Procedures;
using OnionWebApiStarterKit.Core.Services;

namespace OnionWebApiStarterKit.Services.Command
{
    public class UpdateStudentCommandHandler : IDatabaseService, IAsyncRequestHandler<UpdateStudentCommand, Student>
    {
        private readonly IDbContextScopeFactory _dbContextScopeFactory;
        private readonly DoesStudentFirstMidLastNameAlreadyExist _studentNameExistsProcedure;

        public UpdateStudentCommandHandler(IDbContextScopeFactory dbContextScopeFactory, DoesStudentFirstMidLastNameAlreadyExist studentNameExistsProcedure)
        {
            _dbContextScopeFactory = dbContextScopeFactory;
            _studentNameExistsProcedure = studentNameExistsProcedure;
        }

        public async Task<Student> Handle(UpdateStudentCommand command)
        {
            using (var dbContextScope = _dbContextScopeFactory.Create())
            {
                // Gets our context from our context scope
                var dbCtx = dbContextScope.DbContexts.GetByInterface<ISchoolDbContext>();

                var currentStudent = await dbCtx.Students.FindAsync(command.Id);

                if(!currentStudent.FirstMidName.Equals(command.FirstMidName)
                    || !currentStudent.LastName.Equals(command.LastName))
                {
                    // Check if the new first and last names are already taken
                    if (await _studentNameExistsProcedure.HandleAsync<ISchoolDbContext>(command.FirstMidName, command.LastName))
                        throw new RecoverableException("Student FirstMid and LastName Already Exists");

                    currentStudent.FirstMidName = command.FirstMidName;
                    currentStudent.LastName = command.LastName;

                    dbCtx.Entry(currentStudent).State = EntityState.Modified;

                    await dbContextScope.SaveChangesAsync();

                    // This student will have the Id field populated.
                    return currentStudent;
                }

                throw new InvalidOperationException("User didn't change any editable fields. Only FirstMid or Last name can be changed");
            }
        }
    }
}
