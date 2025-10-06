using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Online_Exam_System.Models;
using Online_Exam_System.Models.Questions;

namespace Online_Exam_System.Data
{
    public class OnlineExamContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        // DbSets
        public DbSet<Diploma> Diplomas { get; set; }
        public DbSet<Exam> Exams { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Choice> Choices { get; set; }
        public DbSet<Answer> Answers { get; set; }

        public OnlineExamContext(DbContextOptions<OnlineExamContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // ✅ Always call base first to configure Identity properly
            base.OnModelCreating(modelBuilder);

            // ✅ Apply all IEntityTypeConfiguration from current assembly
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(OnlineExamContext).Assembly);

            // ✅ Optional: Customize Identity table names if you want clean names
            modelBuilder.Entity<ApplicationUser>(entity =>
            {
                entity.ToTable("Users");
            });

            modelBuilder.Entity<IdentityRole<Guid>>(entity =>
            {
                entity.ToTable("Roles");
            });

            modelBuilder.Entity<IdentityUserRole<Guid>>(entity =>
            {
                entity.ToTable("UserRoles");
            });

            modelBuilder.Entity<IdentityUserClaim<Guid>>(entity =>
            {
                entity.ToTable("UserClaims");
            });

            modelBuilder.Entity<IdentityUserLogin<Guid>>(entity =>
            {
                entity.ToTable("UserLogins");
            });

            modelBuilder.Entity<IdentityRoleClaim<Guid>>(entity =>
            {
                entity.ToTable("RoleClaims");
            });

            modelBuilder.Entity<IdentityUserToken<Guid>>(entity =>
            {
                entity.ToTable("UserTokens");
            });
        }
    }
}
