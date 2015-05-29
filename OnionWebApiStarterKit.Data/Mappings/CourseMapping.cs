using OnionWebApiStarterKit.Core.DomainModels;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace OnionWebApiStarterKit.Data.Mappings
{
    public class CourseMapping : EntityTypeConfiguration<Course>
    {
        public CourseMapping()
        {
            this.HasKey(t => t.CourseId);

            this.Property(t => t.CourseId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .IsRequired();

            this.Property(t => t.Title)
                .HasMaxLength(50)
                .IsRequired();
        }
    }
}
