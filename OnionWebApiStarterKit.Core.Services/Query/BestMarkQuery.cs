using OnionWebApiStarterKit.Core.DomainModels;
using MediatR;
using System.Collections.Generic;

namespace OnionWebApiStarterKit.Core.Services.Query
{
    public class BestMarkQuery : IRequest<Enrollment>
    {
        public int StudentId { get; set; }
    }
}
