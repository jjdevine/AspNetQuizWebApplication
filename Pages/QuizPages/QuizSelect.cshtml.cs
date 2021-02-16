using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QuizWebApplication.EntityFramework;
using QuizWebApplication.Extensions;
using QuizWebApplication.Models;
using QuizWebApplication.Services;

namespace QuizWebApplication.Pages.QuizPages
{
    [Authorize]
    public class QuizSelectModel : PageModel
    {
        private readonly IQuizRepository QuizRepository;
        public QuizSelectModel(IQuizRepository quizRepository)
        {
            this.QuizRepository = quizRepository;
        }

        public List<Quiz> UserQuizzes;

        private readonly List<String> errorList = new List<string>();
        public List<String> ErrorList { get { return errorList; } }

        private readonly List<String> infoList = new List<string>();
        public List<String> InfoList { get { return infoList; } }

        public String Username { get; set; }

        public void OnGet(string quizId)
        {
            Username = UserUtils.GetUserFriendlyName(User);
            string delete = HttpContext.Request.Query["delete"];

            var userId = UserUtils.GetUserSubject(User);
            UserQuizzes = QuizRepository.LoadQuizzesForUser(userId);

            if (userId != null && string.Equals(delete, "y", StringComparison.OrdinalIgnoreCase))
            {
                DeleteQuiz(quizId, userId);
            }
        }

        private void DeleteQuiz(string quizId, string userId)
        {
            Guid.TryParse(quizId, out Guid quizIdGuid);

            if(quizIdGuid == null)
            {
                return;
            }

            //check quiz is owned by logged in user
            IEnumerable<Quiz> quizToDelete = from userQuiz in UserQuizzes
                                                where userQuiz.Username.ToLower().Equals(userId.ToLower())
                                                && userQuiz.Id.Equals(quizIdGuid)
                                                select userQuiz;

            if (quizToDelete.Count() > 0 && QuizRepository.DeleteQuiz(quizIdGuid))
            {
                InfoList.Add($"Quiz '{quizToDelete.First().QuizName}' was deleted!");
                UserQuizzes = QuizRepository.LoadQuizzesForUser(userId);
            }
            else
            {
                ErrorList.Add($"Unable to delete quiz {quizId}");
            }
        }
    }
}
