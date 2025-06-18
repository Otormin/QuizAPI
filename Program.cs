
using _netstore.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using QuizWebApiProject.Data;
using QuizWebApiProject.Interfaces;
using QuizWebApiProject.Model;
using QuizWebApiProject.Repository;
using QuizWebApiProject.Services;
using System.Text;
using System.Text.Json.Serialization;

namespace QuizWebApiProject
{
    public class Program
    {
        //cahnge it from::  public static void Main(string[] args) to::  public static async Task Main(string[] args)
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            //for auto mapper
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            //Add this so that you won,t get stuck in a loop when you run your program 
            builder.Services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
            builder.Services.AddScoped<IQuestionAndAnswerRepository, QuestionAndAnswerRepository>();
            builder.Services.AddScoped<ICourseRepository, CourseRepository>();
            builder.Services.AddScoped<ITeacherRepository, TeacherRepository>();
            builder.Services.AddScoped<IStudentScoreRepository, StudentScoreRepository>();
            builder.Services.AddScoped<IAnsweredQuestionRepository, AnsweredQuestionRepository>();
            builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            //for accessing the current logged in user
            builder.Services.AddHttpContextAccessor();

            //for email sending
            //we use AddTransiet to initialize an object whenever we need or with each request
            builder.Services.AddTransient<IMailService, MailService>();

            //for identity framework
            //adding configuration for identity
            builder.Services.AddIdentityCore<User>(opt =>
            {
                //reducing the complexity of a password
                opt.Password.RequireNonAlphanumeric = false;
                //this prevents us from having duplicate emails in our database
                opt.User.RequireUniqueEmail = true;
                //allowing the username to contain this slash
                opt.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789/";
            })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<DataContext>();
            // leave it as builder.Services.AddAuthentication(); if you dont have a jwt
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(opt =>
                {
                    opt.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.
                            GetBytes(builder.Configuration["JWTSettings:TokenKey"])),
                    };
                });



            //very important for identity framework
            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("TeacherPolicy", policy => policy.RequireRole("Teacher"));
                options.AddPolicy("StudentPolicy", policy => policy.RequireRole("Student"));
            });


            //for JwtToken
            builder.Services.AddScoped<TokenService>();


            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();

            //for JwtToken
            builder.Services.AddSwaggerGen(c =>
            {
                var jwtSecurityScheme = new OpenApiSecurityScheme
                {
                    BearerFormat = "JWT",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = JwtBearerDefaults.AuthenticationScheme,
                    Description = "Put Bearer + your token in the box below",
                    Reference = new OpenApiReference
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme
                    }
                };

                c.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        jwtSecurityScheme, Array.Empty<string>()
                    }
                });
            });



            //adding DbContext
            builder.Services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();

                //for JwtToken
                app.UseSwaggerUI(c =>
                {
                    c.ConfigObject.AdditionalItems.Add("persistAuthorization", "true");
                });
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();



            // Seeding identity
            app.MapControllers();

            var scope = app.Services.CreateScope();
            var serviceProvider = scope.ServiceProvider;
            var context = serviceProvider.GetRequiredService<DataContext>();
            var userManager = serviceProvider.GetRequiredService<UserManager<User>>();
            var logger = serviceProvider.GetRequiredService<ILogger<Program>>();
            try
            {
                await context.Database.MigrateAsync();
                var seed = new Seed(context, userManager);
                await seed.SeedUsersAsync(app);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "A problem occurred during migration or seeding");
            }




            app.Run();
        }
    }
}
