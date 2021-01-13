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
        private const string Key = "SESSION_STATE";

        public static SessionState GetSessionState(ISession session)
        {

            var sessionState = session.GetObject<SessionState>(Key);

            if(sessionState == null)
            {
                sessionState = new SessionState();
                session.SetObject(Key, sessionState);
            }

            return sessionState;
        }

        public static void UpdateSessionState(ISession session, SessionState sessionState)
        {
            session.SetObject(Key, sessionState);
        }

        public static void SetActiveQuiz(ISession session, Guid quizId)
        {
            var sessionState = GetSessionState(session);
            sessionState.ActiveQuizId = quizId;
            UpdateSessionState(session, sessionState);
        }
    }
}
