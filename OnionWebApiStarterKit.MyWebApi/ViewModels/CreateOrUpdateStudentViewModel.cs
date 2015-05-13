using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnionWebApiStarterKit.MyWebApi.ViewModels
{
    public class CreateOrUpdateStudentViewModel
    {
        public string LastName { get; set; }
        public string FirstMidName { get; set; }
        public DateTime EnrollmentDate { get; set; }
    }
}