using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Online_Exam_System.Models.Questions;

namespace Online_Exam_System.Data.Configurations
{
    public class QuestionConfigrations : IEntityTypeConfiguration<Question>
    {
        public void Configure(EntityTypeBuilder<Question> builder)
        {
            builder.ToTable("Questions");

            
            builder.HasKey(q => q.Id);

            
            builder.Property(q => q.Title)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(q => q.Type)
                .IsRequired()
                .HasMaxLength(50);

       
            builder.HasOne(q => q.Exam)
                .WithMany(e => e.Questions)
                .HasForeignKey(q => q.ExamId)
                .OnDelete(DeleteBehavior.Cascade);

            
            builder.HasMany(q => q.Choices)
                .WithOne(c => c.Question)
                .HasForeignKey(c => c.QuestionId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
