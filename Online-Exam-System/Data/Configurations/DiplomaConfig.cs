using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Online_Exam_System.Models;

namespace Online_Exam_System.Data.Configurations
{
    public class DiplomaConfig : IEntityTypeConfiguration<Diploma>
    {
        public void Configure(EntityTypeBuilder<Diploma> builder)
        {
            builder.HasKey(e => e.id);

            builder.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(e => e.Description)
                 .IsRequired(false)
                 .HasMaxLength(200);

            builder.Property(e => e.PictureUrl)
                  .IsRequired(false);

        }
    }
}
