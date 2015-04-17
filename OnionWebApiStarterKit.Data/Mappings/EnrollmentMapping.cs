using OnionWebApiStarterKit.Core.DomainModels;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace OnionWebApiStarterKit.Data.Mappings
{
    public class EnrollmentMapping : EntityTypeConfiguration<Enrollment>
    {
        public EnrollmentMapping()
        {
            this.HasKey(t => t.EnrollmentId);

            this.Property(t => t.EnrollmentId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)

                .IsRequired();
            this.Property(t => t.StudentId)
                .IsRequired();

            this.Property(t => t.CourseId)
                .IsRequired();

            // Foreign Key
            this.HasRequired(x => x.Student)
                .WithMany()
                .HasForeignKey(x => x.StudentId);

            this.HasRequired(x => x.Course)
                .WithMany()
                .HasForeignKey(x => x.CourseId);
        }
    }
}
