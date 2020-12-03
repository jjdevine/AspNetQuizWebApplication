using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QuizWebApplication.Models;

namespace QuizWebApplication.Data
{
    public class QuizWebApplicationContext : DbContext
    {
        public QuizWebApplicationContext (DbContextOptions<QuizWebApplicationContext> options)
            : base(options)
        {
        }

        public DbSet<QuizWebApplication.Models.DataModel1> DataModel1 { get; set; }
    }
}
