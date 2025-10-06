using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Online_Exam_System.Models.Questions;

namespace Online_Exam_System.Data.Configurations
{
    public class AnswerConfigrations : IEntityTypeConfiguration<Answer>
    {
        public void Configure(EntityTypeBuilder<Answer> builder)
        {
            builder.ToTable("Answers");

          
            builder.HasKey(a => a.Id);

            builder.Property(a => a.UserId)
                .HasMaxLength(100);

            builder.HasOne(a => a.Question)
                .WithMany() 
                .HasForeignKey(a => a.QuestionId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.Choice)
                .WithMany() 
                .HasForeignKey(a => a.ChoiceId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
