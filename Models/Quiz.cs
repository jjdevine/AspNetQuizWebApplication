using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizWebApplication.Models
{
    public class Quiz
    {
        public Guid Id { get; set; }

        public string Username { get; set; }

        public string QuizName { get; set; }
    }
}
