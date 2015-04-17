using OnionWebApiStarterKit.Core.DomainModels;
using System.Data.Entity;

namespace OnionWebApiStarterKit.Data
{
    public interface ISchoolDbContext : IDbContext
    {
        DbSet<Student> Students { get; }
        DbSet<Course> Courses { get; }
        DbSet<Enrollment> Enrollments { get; }
    }
}
