using Microsoft.EntityFrameworkCore;
using QuizWebApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizWebApplication.EntityFramework
{
    public class QuizContext : DbContext
    {
        public QuizContext(DbContextOptions<QuizContext> options) : base(options)
        {

        }

        public DbSet<Quiz> Quizzes { get; set; }
        public DbSet<QuizQuestion> QuizQuestions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Quiz>()
                .ToTable("UserQuizzes", "quiz")
                .Property(q => q.Id).HasColumnName("QuizId");
            modelBuilder.Entity<Quiz>().Property(q => q.Username).HasColumnName("User");
            modelBuilder.Entity<QuizQuestion>().ToTable("QuizQuestions", "quiz");
        }
    }

}
