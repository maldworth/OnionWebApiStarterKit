using MediatR;
using OnionWebApiStarterKit.Core.DomainModels;
using OnionWebApiStarterKit.Core.Dto;
using OnionWebApiStarterKit.Core.Services.Command;
using OnionWebApiStarterKit.Core.Services.Query;
using OnionWebApiStarterKit.MyWebApi.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace OnionWebApiStarterKit.MyWebApi.Controllers.api
{
    public class StudentsController : ApiController
    {
        private readonly IMediator _mediator;

        public StudentsController(IMediator mediator)
        {
            if (mediator == null)
                throw new ArgumentNullException("mediator");
            _mediator = mediator;
        }

        public async System.Threading.Tasks.Task<IEnumerable<Student>> Get()
        {
            // Example of calling the paginated result
            //var pagedStudents = _mediator.Send(new PaginateQuery<Student>(2,3,orderBy: x=>x.OrderBy(c=>c.LastName)));

            // Depending on your design decisions, you might want to limit the amount of results that can be queries.
            // We made our services require a "Take" amount. You may not want to adopt this approach.
            var students = await _mediator.SendAsync(new AsyncGenericQuery<Student>(20));
            return students;
        }

        // I know this method is supposed to return the student's details, but I've just chosen to use this as an example of comparing the broad service AutoMapperQuery versus the specific BestMarkInCourse, and both achieve the same result albeit differently.
        public dynamic Get(int id)
        {
            // This query is almost identical to our BestMarkInCourse that we used in the odata function, but there's one difference. This result will return an ienumerable of 1 (by using the .Take(pageSize)), then calls FirstOrDefault
            // Versus, the BestMarkInCourseHandler, which actually calls the FirstOrDefault to trigger the DB Query.
            // So really, just two different ways to perform this query
            var bestMark = _mediator.Send(new AutoMapperQuery<Enrollment, BestMarkInCourseDto>(1, x => x.OrderBy(y => y.Grade), x => x.StudentId == id && x.Grade != null));
            return bestMark.FirstOrDefault();
        }

        // This method is also an example of a webapi which doesn't want to expose the domain model. So students are created with a specific view model,
        // and so we need to perform fluent validation on that view model. It may seem redundant to validate this model, then our service validates the command as well (which is the same in this case).
        // But depending on your implementation, your service might allow more configurations, or it could be an "CreateOrUpdate" service, and so you would want to do some preliminary validation
        // here before calling the service. So really I'm just trying to show all potential ways to use these features. Your project architecture, complexity and code style/conventions might
        // favor one more than the other, but the building blocks are here.
        public async Task<dynamic> Post([FromBody]CreateStudentViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var command = new CreateStudentCommand
            {
                FirstMidName = model.FirstMidName,
                LastName = model.LastName,
                EnrollmentDate = model.EnrollmentDate
            };

            var student = await _mediator.SendAsync(command);

            return student.Id;
        }

        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //public void Delete(int id)
        //{
        //}
    }
}
