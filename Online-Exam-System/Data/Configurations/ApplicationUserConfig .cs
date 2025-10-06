namespace Online_Exam_System.Data.Configurations
{
    using global::Online_Exam_System.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    namespace Online_Exam_System.Data.Configurations
    {
        public class ApplicationUserConfig : IEntityTypeConfiguration<ApplicationUser>
        {
            public void Configure(EntityTypeBuilder<ApplicationUser> builder)
            {
                builder.Property(u => u.FullName)
                    .IsRequired()
                    .HasMaxLength(150);
                
                builder.Property(u => u.FullName)
                        .HasMaxLength(150)
                        .HasComputedColumnSql("[FirstName] + ' ' + [LastName]");


                builder.HasMany(u => u.Diplomas)
                    .WithOne(d => d.User)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.SetNull);

                builder.HasMany(u => u.Exams)
                    .WithOne(e => e.User)
                    .HasForeignKey(e => e.UserId)
                    .OnDelete(DeleteBehavior.SetNull);
            }
        }
    }

}
