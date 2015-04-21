using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Threading.Tasks;
using OnionWebApiStarterKit.Core.DomainModels;
using System.ComponentModel.DataAnnotations;
using OnionWebApiStarterKit.Data.Mappings;
using System;

namespace OnionWebApiStarterKit.Data
{
    public class SchoolDbContext : DbContext, ISchoolDbContext
    {
        public SchoolDbContext()
            : base("name=SchoolContext") // use app.config transforms or web.config transforms to change this in your UI/Presentation project
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new StudentMapping());
            modelBuilder.Configurations.Add(new EnrollmentMapping());
            modelBuilder.Configurations.Add(new CourseMapping());
        }

        // ISchoolDbContext implementation
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
    }
}
