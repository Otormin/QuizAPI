using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using QuizWebApiProject.Model;
using System.Collections.Generic;
using System.Diagnostics.Metrics;

namespace QuizWebApiProject.Data
{
    //before it was:: public class DataContext : DbContext
    //but because we are using identity framework it is now:: public class DataContext : IdentityDbContext<(Name of model that is inheriting from IdentityUser)>
    public class DataContext : IdentityDbContext<User>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        //the variable name should be pluralized
        public DbSet<Course> Courses { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<QuestionAndAnswer> QuestionsAndAnswers { get; set; }
        public DbSet<StudentScore> StudentScores { get; set; }  
        public DbSet<AnsweredQuestion> AnsweredQuestions { get; set; }


        //identity framework
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);


            builder.Entity<IdentityRole>()
                .HasData(
                    new IdentityRole { Name = "Student", NormalizedName = "STUDENT" },
                    new IdentityRole { Name = "Teacher", NormalizedName = "TEACHER" }
                 );
        }
    }
} 
