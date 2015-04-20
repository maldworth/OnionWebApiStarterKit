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
        private static readonly object Lock = new object();
        private static bool _databaseInitialized;

        public SchoolDbContext()
            : base("name=SchoolContext") // use app.config transforms or web.config transforms to change this
        {
            if (_databaseInitialized)
            {
                return;
            }
            lock (Lock)
            {
                if (!_databaseInitialized)
                {
                    // Set the database intializer which is run once during application start
                    // This seeds the database with admin user credentials and admin role
                    Database.SetInitializer(new ApplicationDbInitializer());
                    _databaseInitialized = true;
                }
            }
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
