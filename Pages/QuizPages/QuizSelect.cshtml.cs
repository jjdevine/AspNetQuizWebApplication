using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QuizWebApplication.EntityFramework;
using QuizWebApplication.Extensions;
using QuizWebApplication.Models;
using QuizWebApplication.Services;

namespace QuizWebApplication.Pages.QuizPages
{
    public class QuizSelectModel : PageModel
    {
        private readonly IQuizRepository QuizRepository;
        private readonly QuizContext QuizContext;
        public QuizSelectModel(IQuizRepository quizRepository, QuizContext quizContext)
        {
            this.QuizRepository = quizRepository;
            this.QuizContext = quizContext;
        }

        public List<Quiz> UserQuizzes;

        private readonly List<String> errorList = new List<string>();
        public List<String> ErrorList { get { return errorList; } }

        private readonly List<String> infoList = new List<string>();
        public List<String> InfoList { get { return infoList; } }

        public SessionState Session { get { return SessionUtils.GetSessionState(HttpContext.Session); } }

        public void OnGet(string quizId)
        {
            string delete = HttpContext.Request.Query["delete"];

            var username = SessionUtils.GetSessionState(HttpContext.Session)?.Username;
            UserQuizzes = QuizRepository.LoadQuizzesForUser(username);

            //TODO: select some records from the DB
            //https://docs.microsoft.com/en-us/aspnet/core/data/ef-rp/crud?view=aspnetcore-5.0
           // QuizContext.Quizzes.W


            if (username != null && string.Equals(delete, "y", StringComparison.OrdinalIgnoreCase))
            {
                DeleteQuiz(quizId, username);
            }
        }

        private void DeleteQuiz(string quizId, string username)
        {
            Guid.TryParse(quizId, out Guid quizIdGuid);

            if(quizIdGuid == null)
            {
                return;
            }

            //check quiz is owned by logged in user
            IEnumerable<Quiz> quizToDelete = from userQuiz in UserQuizzes
                                                where userQuiz.Username.ToLower().Equals(username.ToLower())
                                                && userQuiz.Id.Equals(quizIdGuid)
                                                select userQuiz;

            if (quizToDelete.Count() > 0 && QuizRepository.DeleteQuiz(quizIdGuid))
            {
                InfoList.Add($"Quiz '{quizToDelete.First().QuizName}' was deleted!");
                UserQuizzes = QuizRepository.LoadQuizzesForUser(username);
            }
            else
            {
                ErrorList.Add($"Unable to delete quiz {quizId}");
            }
        }
    }
}
