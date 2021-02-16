using Microsoft.AspNetCore.Http;
using QuizWebApplication.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizWebApplication.Extensions
{
    public class SessionUtils
    {
        private const string KEY_ACTIVE_QUIZ = "ACTIVE_QUIZ";

        public static void SetActiveQuiz(ISession session, Guid quizId)
        {
            session.SetString(KEY_ACTIVE_QUIZ, quizId.ToString());
        }

        public static Guid? GetActiveQuiz(ISession session)
        {
            string activeSessionStr = session.GetString(KEY_ACTIVE_QUIZ);

            bool activeQuizExists = Guid.TryParse(activeSessionStr, out Guid result);

            if (activeQuizExists)
            {
                return result;
            }
            else
            {
                return null;
            }
        }
    }
}
