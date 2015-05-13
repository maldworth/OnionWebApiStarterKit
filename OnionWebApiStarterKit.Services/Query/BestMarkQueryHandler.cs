using MediatR;
using Mehdime.Entity;
using OnionWebApiStarterKit.Data.Extensions;
using System.Collections.Generic;
using System;
using System.Linq;
using OnionWebApiStarterKit.Core.DomainModels;
using System.Data.Entity;
using OnionWebApiStarterKit.Core.Services.Query;
using OnionWebApiStarterKit.Data;
using OnionWebApiStarterKit.Core.Services;

namespace OnionWebApiStarterKit.Services.Query
{
    public class BestMarkQueryHandler : IRequestHandler<BestMarkQuery, Enrollment>
    {
        private readonly IDbContextScopeFactory _dbContextScopeFactory;

        public BestMarkQueryHandler(IDbContextScopeFactory dbContextScopeFactory)
        {
            if (dbContextScopeFactory == null)
                throw new ArgumentNullException("dbContextScopeFactory");

            _dbContextScopeFactory = dbContextScopeFactory;
        }

        public Enrollment Handle(BestMarkQuery query)
        {
            using (var dbContextScope = _dbContextScopeFactory.CreateReadOnly())
            {
                var dbCtx = dbContextScope.DbContexts.GetByInterface<ISchoolDbContext>();

                ((DbContext)dbCtx).Configuration.ProxyCreationEnabled = false;

                var enrollment = dbCtx.Enrollments.Where(x => x.StudentId == query.StudentId && x.Grade != null).OrderBy(x => x.Grade).FirstOrDefault();
                if(enrollment == null)
                {
                    throw new InvalidOperationException("No enrollments found for this student");
                }

                return enrollment;
            }
        }
    }
}
