using OnionWebApiStarterKit.Core.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnionWebApiStarterKit.MyWebApi.ViewModels
{
    public class StudentCourseListViewModel
    {
        public string LastName { get; set; }
        public string FirstMidName { get; set; }
        public IEnumerable<CourseDetailsViewModel> Enrollments { get; set; }
    }

    public class CourseDetailsViewModel
    {
        public string CourseTitle { get; set; }
    }
}