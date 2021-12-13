using AskQuestion.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AskQuestion.DataAccess.Concrete.EntityFramework
{
    public class AskQuestionContext : DbContext
    {

        public DbSet<User> Users { get; set; }

        public DbSet<Question> Questions { get; set; }

        public DbSet<AskedQuestion> AskedQuestions { get; set; }

        public DbSet<UserQuestion> UserQuestions { get; set; }

        public DbSet<QuestionAnswer> QuestionAnswers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=MASAUSTU\\SQLEXPRESS;Database=AskQuestion;Trusted_Connection=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

    }
}
