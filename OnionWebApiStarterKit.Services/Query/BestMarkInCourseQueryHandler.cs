using MediatR;
using Mehdime.Entity;
using OnionWebApiStarterKit.Data.Extensions;
using System.Collections.Generic;
using System;
using System.Linq;
using OnionWebApiStarterKit.Core.DomainModels;
using OnionWebApiStarterKit.Core.Dto;
using AutoMapper.QueryableExtensions;
using OnionWebApiStarterKit.Core.Services.Query;
using OnionWebApiStarterKit.Data;

namespace OnionWebApiStarterKit.Services.Query
{
    /// <summary>
    /// This is an example of showing a projection onto a Dto. The same thing could have been achieved using AutoMapperQuery from a Model provided in our UI Layer.
    /// </summary>
    public class BestMarkInCourseQueryHandler : IRequestHandler<BestMarkInCourseQuery, BestMarkInCourseDto>
    {
        private readonly IDbContextScopeFactory _dbContextScopeFactory;

        public BestMarkInCourseQueryHandler(IDbContextScopeFactory dbContextScopeFactory)
        {
            if (dbContextScopeFactory == null)
                throw new ArgumentNullException("dbContextScopeFactory");

            _dbContextScopeFactory = dbContextScopeFactory;
        }

        public BestMarkInCourseDto Handle(BestMarkInCourseQuery query)
        {
            using (var dbContextScope = _dbContextScopeFactory.CreateReadOnly())
            {
                var dbCtx = dbContextScope.DbContexts.GetByInterface<ISchoolDbContext>();

                var enrollmentDto = dbCtx.Enrollments.Project().To<BestMarkInCourseDto>().Where(x => x.StudentId == query.StudentId && x.Grade != null).OrderBy(x => x.Grade).FirstOrDefault();
                if (enrollmentDto == null)
                {
                    throw new InvalidOperationException("No enrollments found for this student");
                }

                return enrollmentDto;
            }
        }
    }
}
