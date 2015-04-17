using OnionWebApiStarterKit.Core.DomainModels;
namespace OnionWebApiStarterKit.Core.Dto
{
    public class BestMarkInCourseDto
    {
        // We make all of these elements match Enrollment, so AutoMapper has a nice time projecting onto it
        public int EnrollmentId { get; set; }
        public int CourseId { get; set; }
        public int StudentId { get; set; }
        public Grade? Grade { get; set; }

        // Uses AutoMapper feature of (Course.Title) will match to (CourseTitle)
        public string CourseTitle { get; set; }
    }
}
