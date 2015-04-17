using OnionWebApiStarterKit.Core.DomainModels;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace OnionWebApiStarterKit.Data.Mappings
{
    public class StudentMapping : EntityTypeConfiguration<Student>
    {
        public StudentMapping()
        {
            this.HasKey(t => t.Id);

            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.FirstMidName)
                .HasMaxLength(50)
                .IsRequired();

            this.Property(t => t.LastName)
                .HasMaxLength(50)
                .IsRequired();

            // Navigation Property
            this.HasMany(x => x.Enrollments)
                .WithRequired(x=>x.Student)
                .WillCascadeOnDelete(false);
        }
    }
}
