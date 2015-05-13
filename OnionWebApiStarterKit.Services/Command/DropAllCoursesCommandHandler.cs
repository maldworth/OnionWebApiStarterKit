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
using OnionWebApiStarterKit.Core.Services;

namespace OnionWebApiStarterKit.Services.Command
{
    public class DropAllCoursesCommandHandler : RequestHandler<DropAllCoursesCommand>, IDatabaseService
    {
        private readonly IDbContextScopeFactory _dbContextScopeFactory;

        public DropAllCoursesCommandHandler(IDbContextScopeFactory dbContextScopeFactory)
        {
            _dbContextScopeFactory = dbContextScopeFactory;
        }

        protected override void HandleCore(DropAllCoursesCommand command)
        {
            using (var dbContextScope = _dbContextScopeFactory.Create())
            {
                // Gets our context from our context scope
                var dbCtx = dbContextScope.DbContexts.GetByInterface<ISchoolDbContext>();

                // Gets all enrollments that match student ID. Could have verified StudentID exists, but chose not to.
                var enrollments = dbCtx.Enrollments.Where(x => x.StudentId == command.StudentId).ToList();
                if(enrollments == null || enrollments.Count <= 0)
                {
                    throw new InvalidOperationException("No Enrollments found to drop.");
                }

                dbCtx.Enrollments.RemoveRange(enrollments);

                dbContextScope.SaveChanges();
            }
        }
    }
}
