using AutoMapper;
using OnionWebApiStarterKit.Core.DomainModels;
using OnionWebApiStarterKit.MyWebApi.ViewModels;
using System.Collections.Generic;

namespace OnionWebApiStarterKit.MyWebApi.AutoMapperProfiles
{
    public class AutoMapperProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<Student, StudentCourseListViewModel>();
            Mapper.CreateMap<Enrollment, CourseDetailsViewModel>();
        }
    }
}