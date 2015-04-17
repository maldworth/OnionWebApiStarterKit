using System.Collections.Generic;
namespace OnionWebApiStarterKit.Core.DomainModels
{
    public class Course
    {
        public int CourseId { get; set; }
        public string Title { get; set; }
        public int Credits { get; set; }

        public virtual ICollection<Enrollment> Enrollments { get; set; }
    }
}
