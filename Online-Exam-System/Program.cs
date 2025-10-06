using FluentValidation;
using MediatR;
using MediatR.Extensions.FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Online_Exam_System.Contarcts;
using Online_Exam_System.Data;
using Online_Exam_System.Data.Seed;
using Online_Exam_System.Features.Auth.Login.Online_Exam_System.Features.Auth.Login;
using Online_Exam_System.Features.Auth.Register;
using Online_Exam_System.Features.Auth.UpdateUserProfile;
using Online_Exam_System.Features.Diploma.AddDiploma;
using Online_Exam_System.Features.Diploma.DeleteDiploma;
using Online_Exam_System.Features.Diploma.UpdateDiploma;
using Online_Exam_System.Features.Exam.AddExam;
using Online_Exam_System.Features.Exam.DeleteExam;
using Online_Exam_System.Features.Exam.GetAll;
using Online_Exam_System.Features.Exam.UpdateExam;
using Online_Exam_System.Models;
using Online_Exam_System.Repositories;
using Online_Exam_System.Services;
using System.Reflection;
using System.Text;
using System.Text.Json;

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
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Online Exam API", Version = "v1" });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<OnlineExamContext>(options =>
                options.UseSqlServer(connectionString, opts =>
                    opts.CommandTimeout((int)TimeSpan.FromMinutes(10).TotalSeconds)));

            builder.Services.AddIdentity<ApplicationUser, IdentityRole<Guid>>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 6;
            })
            .AddEntityFrameworkStores<OnlineExamContext>()
            .AddDefaultTokenProviders();

            var jwtSettings = builder.Configuration.GetSection("JwtSettings");
            var secretKey = jwtSettings["SecretKey"];
            if (!string.IsNullOrEmpty(secretKey))
            {
                builder.Services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = "JwtBearer";
                    options.DefaultChallengeScheme = "JwtBearer";
                })
                .AddJwtBearer("JwtBearer", options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtSettings["Issuer"],
                        ValidAudience = jwtSettings["Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
                    };

                    options.Events = new Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerEvents
                    {
                        OnChallenge = context =>
                        {
                            context.HandleResponse();
                            context.Response.StatusCode = 401;
                            context.Response.ContentType = "application/json";
                            var result = JsonSerializer.Serialize(new
                            {
                                statusCode = 401,
                                message = "You are not authenticated. Please provide a valid token."
                            });
                            return context.Response.WriteAsync(result);
                        },
                        OnForbidden = context =>
                        {
                            context.Response.StatusCode = 403;
                            context.Response.ContentType = "application/json";
                            var result = JsonSerializer.Serialize(new
                            {
                                statusCode = 403,
                                message = "You are not authorized to access this resource."
                            });
                            return context.Response.WriteAsync(result);
                        }
                    };
                });
            }

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
            });

            // DI
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IImageHelper, ImageHelper>();
            builder.Services.AddScoped<IAddExamOrchestrator, AddExamOrchestrator>();
            builder.Services.AddScoped<IUpdateExamOrchestrator, UpdateExamOrchestrator>();
            builder.Services.AddScoped<IDeleteExamCommandOrchestrator, DeleteExamOrchestrator>();
            builder.Services.AddScoped<IAddDiplomaOrchestrator, AddDiplomaOrchestrator>();
            builder.Services.AddScoped<IUpdateDiplomaOrchestrator, UpdateDiplomaOrchestrator>();
            builder.Services.AddScoped<IDeleteDiplomaOrchestrator, DeleteDiplomaOrchestrator>();
            builder.Services.AddScoped<UpdateUserProfileOrchestrator>();
            builder.Services.AddScoped<ITokenService, JwtService>();
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddMemoryCache();

            builder.Services.AddMediatR(Assembly.GetAssembly(typeof(GetAllExamHandler)));
            builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            builder.Services.AddValidatorsFromAssembly(typeof(RegisterCommandValidator).Assembly);
            builder.Services.AddValidatorsFromAssembly(typeof(LoginValidator).Assembly);

            #endregion

            var app = builder.Build();

            #region Database Seeding
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<OnlineExamContext>();
                    var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
                    var roleManager = services.GetRequiredService<RoleManager<IdentityRole<Guid>>>();

                    await context.Database.MigrateAsync();
                    await IdentitySeeder.SeedIdentityAsync(roleManager, userManager);
                    await DiplomaSeeder.SeedDiplomasAsync(context);
                    await ExamSeeder.SeedExamsAsync(context);
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "Error occurred while seeding initial data");
                }
            }
            #endregion

            #region Middleware
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseStaticFiles();
            app.UseHttpsRedirection();
            app.UseMiddleware<Middlewares.GlobalExceptionMiddleware>();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
            #endregion

            await app.RunAsync();
        }
    }
}
