using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QuizWebApplication.Extensions;
using QuizWebApplication.Models;
using QuizWebApplication.Services;

namespace QuizWebApplication.Pages.QuizPages
{
    public class QuizSelectModel : PageModel
    {
        private readonly IQuizRepository QuizRepository;
        public QuizSelectModel(IQuizRepository quizRepository)
        {
            this.QuizRepository = quizRepository;
        }

        public List<Quiz> UserQuizzes;

        public SessionState Session { get { return SessionUtils.GetSessionState(HttpContext.Session); } }

        public void OnGet()
        {
            var username = SessionUtils.GetSessionState(HttpContext.Session).Username;

            UserQuizzes = QuizRepository.LoadQuizzesForUser(username);

            //display quizzes this user has previously created
            //give option to create new quiz
            //give option to play an existing quiz
        }
    }
}
