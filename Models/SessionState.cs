using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizWebApplication.Models
{
    public class SessionState
    {
        public string Username {get; set;}

        public Guid ActiveQuizId { get; set; }
    }
}
