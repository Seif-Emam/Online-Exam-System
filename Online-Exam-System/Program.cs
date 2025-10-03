using MediatR;
using Microsoft.EntityFrameworkCore;
using Online_Exam_System.Contarcts;
using Online_Exam_System.Data;
using Online_Exam_System.Data.Seed;
using Online_Exam_System.Features.Exam.AddExam;
using Online_Exam_System.Features.Exam.GetAll;
using Online_Exam_System.Features.Exam.UpdateExam;
using Online_Exam_System.Repositories;
using Online_Exam_System.Services;
using System.Reflection;

namespace Online_Exam_System
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Services

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<OnlineExamContext>(options =>
                options.UseSqlServer(connectionString, opts =>
                    opts.CommandTimeout((int)TimeSpan.FromMinutes(10).TotalSeconds)));

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IImageHelper, ImageHelper>();
            builder.Services.AddScoped<IAddExamOrchestrator, AddExamOrchestrator>();
            builder.Services.AddScoped<IUpdateExamOrchestrator, UpdateExamOrchestrator>();
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddMemoryCache();
            builder.Services.AddMediatR(Assembly.GetAssembly(typeof(GetAllExamHandler)));

            #endregion

            var app = builder.Build();

            #region Seeding section
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<OnlineExamContext>();
                    await DiplomaSeeder.SeedDiplomasAsync(context);
                    await ExamSeeder.SeedExamsAsync(context);
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "❌ Error while seeding Diplomas");
                }
            }
            #endregion

            #region Configure Middleware

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // ✅ allow serving static files (like images)
            app.UseStaticFiles();

            app.UseHttpsRedirection();
            app.UseMiddleware<Middlewares.GlobalExceptionMiddleware>();

            app.UseAuthorization();
            app.MapControllers();

            #endregion

            await app.RunAsync();
        }
    }
}
