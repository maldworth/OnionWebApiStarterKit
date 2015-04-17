using OnionWebApiStarterKit.Core.DomainModels;
using OnionWebApiStarterKit.Core.Dto;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionWebApiStarterKit.Services.AutoMapperConfig
{
    public class AutoMapperProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<Enrollment, BestMarkInCourseDto>();
        }
    }
}
