using Microsoft.EntityFrameworkCore;
using Online_Exam_System.Models;
using System.Security.Cryptography.X509Certificates;

namespace Online_Exam_System.Data
{
    public class OnlineExamContext : DbContext
    {


        public DbSet<Diploma> Doplomas { set; get; }
        public DbSet<Exam> Exams { set; get; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           base.OnModelCreating(modelBuilder.ApplyConfigurationsFromAssembly(typeof(OnlineExamContext).Assembly));

        }
        public OnlineExamContext(DbContextOptions<OnlineExamContext> options) : base(options)
        {









        }

        // Add DbSet properties for your entities here
        // public DbSet<YourEntity> YourEntities { get; set; }



    }
}
