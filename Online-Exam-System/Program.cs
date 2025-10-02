
using MediatR;
using Microsoft.EntityFrameworkCore;
using Online_Exam_System.Contarcts;
using Online_Exam_System.Features.Exam.GetAll;
using Online_Exam_System.Repositories;
using Online_Exam_System.Services;
using System.Reflection;

namespace Online_Exam_System
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region services

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();



            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<Data.OnlineExamContext>(options =>
                options.UseSqlServer(connectionString, opts => opts.CommandTimeout((int)TimeSpan.FromMinutes(10).TotalSeconds)));
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IImageHelper, ImageHelper>();
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddMemoryCache();
            builder.Services.AddMediatR(Assembly.GetAssembly(typeof(GetAllExamHandler)));



            #endregion

            var app = builder.Build();


            #region Configure 

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            #endregion

            app.Run();
        }
    }
}
