using OnionWebApiStarterKit.Core.DomainModels;
using MediatR;
using System.Collections.Generic;
using OnionWebApiStarterKit.Core.Services.Abstracts;

namespace OnionWebApiStarterKit.Core.Services.Query
{
    public class BestMarkQuery
        : BaseRequest, IRequest<Enrollment>
    {
        public int StudentId { get; set; }
    }
}
