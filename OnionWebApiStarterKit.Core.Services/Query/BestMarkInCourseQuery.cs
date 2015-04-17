using OnionWebApiStarterKit.Core.Dto;
using MediatR;
using System.Collections.Generic;

namespace OnionWebApiStarterKit.Core.Services.Query
{
    public class BestMarkInCourseQuery : IRequest<BestMarkInCourseDto>
    {
        public int StudentId { get; set; }
    }
}
