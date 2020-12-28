using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QuizWebApplication.Extensions;
using QuizWebApplication.Models;

namespace QuizWebApplication.Pages.QuizPages
{
    public class QuizSelectModel : PageModel
    {
        public SessionState Session { get { return SessionUtils.GetSessionState(HttpContext.Session); } }

        public void OnGet()
        {
            Console.WriteLine(SessionUtils.GetSessionState(HttpContext.Session).Username);
            //check if user exists in db
            //otherwise create
            //display quizzes this user has previously created
            //give option to create new quiz
            //give option to play an existing quiz
        }
    }
}
