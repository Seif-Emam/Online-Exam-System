using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Online_Exam_System.Models;

namespace Online_Exam_System.Data.Configurations
{
    public class ExamConfig : IEntityTypeConfiguration<Exam>
    {
        public void Configure(EntityTypeBuilder<Exam> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(e => e.PictureUrl)
                 .IsRequired(false)
                 .HasMaxLength(500);

            builder.Property(e => e.StartDate)
                 .HasConversion(
                    v => v.ToDateTime(TimeOnly.MinValue),
                    v => DateOnly.FromDateTime(v));

            builder.Property(e => e.EndDate)
                 .HasConversion(
                    v => v.ToDateTime(TimeOnly.MinValue),
                    v => DateOnly.FromDateTime(v));

            builder.Property(e => e.Duration)
                .HasConversion(
                    v => v.ToTimeSpan(),
                    v => TimeOnly.FromTimeSpan(v));


            builder.HasOne(e => e.Diploma)
                   .WithMany(d => d.Exams)
                   .HasForeignKey(e => e.DiplomaId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(e => e.User)
                   .WithMany(u => u.Exams)
                   .HasForeignKey(e => e.UserId)
                   .OnDelete(DeleteBehavior.SetNull);


        }
    }
}
