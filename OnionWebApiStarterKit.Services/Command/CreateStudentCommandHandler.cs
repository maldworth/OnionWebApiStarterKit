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
using OnionWebApiStarterKit.Core.Services;

namespace OnionWebApiStarterKit.Services.Command
{
    public class CreateStudentCommandHandler : IDatabaseService, IAsyncRequestHandler<CreateStudentCommand, Student>
    {
        private readonly IDbContextScopeFactory _dbContextScopeFactory;

        public CreateStudentCommandHandler(IDbContextScopeFactory dbContextScopeFactory)
        {
            _dbContextScopeFactory = dbContextScopeFactory;
        }

        public async Task<Student> Handle(CreateStudentCommand command)
        {
            using (var dbContextScope = _dbContextScopeFactory.Create())
            {
                // Gets our context from our context scope
                var dbCtx = dbContextScope.DbContexts.GetByInterface<ISchoolDbContext>();

                // Map our command to a new student entity. We purposely don't use automapping for this. We want to control our mapping in a 1 to 1 manner
                var domainModel = new Student
                {
                    FirstMidName = command.FirstMidName,
                    LastName = command.LastName,
                    EnrollmentDate = command.EnrollmentDate
                };

                dbCtx.Students.Add(domainModel);

                await dbContextScope.SaveChangesAsync();

                // This student will have the Id field populated.
                return domainModel;
            }
        }
    }
}
