using QuizWebApplication.Models;
using System;
using System.Collections.Generic;

namespace QuizWebApplication.Services
{
    public interface IQuizRepository
    {
        List<Quiz> LoadQuizzesForUser(string username);
        bool PersistQuiz(Quiz quiz);
        bool PersistQuizQuestions(List<QuizQuestion> quizQuestions);

        List<QuizQuestion> LoadQuizQuestions(Guid quizId, int listSize, bool randomOrder);
        Quiz LoadQuizById(Guid quizId);
    }
}