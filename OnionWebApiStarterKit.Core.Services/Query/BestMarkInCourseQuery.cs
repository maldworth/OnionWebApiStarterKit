using OnionWebApiStarterKit.Core.Dto;
using MediatR;
using System.Collections.Generic;
using OnionWebApiStarterKit.Core.Services.Abstracts;

namespace OnionWebApiStarterKit.Core.Services.Query
{
    public class BestMarkInCourseQuery
        : BaseRequest,
        IRequest<BestMarkInCourseDto>
    {
        public int StudentId { get; set; }
    }
}
