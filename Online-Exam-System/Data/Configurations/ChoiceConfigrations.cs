using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Online_Exam_System.Models.Questions;

namespace Online_Exam_System.Data.Configurations
{
    public class ChoiceConfigrations : IEntityTypeConfiguration<Choice>
    {
        public void Configure(EntityTypeBuilder<Choice> builder)
        {
            builder.ToTable("Choices");

         
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Content)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(c => c.IsCorrect)
                .IsRequired();

            builder.HasOne(c => c.Question)
                .WithMany(q => q.Choices)
                .HasForeignKey(c => c.QuestionId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
