using QuizWebApplication.Models;
using System.Collections.Generic;

namespace QuizWebApplication.Services
{
    public interface IQuizRepository
    {
        bool PersistQuiz(Quiz quiz);
        bool PersistQuizQuestions(List<QuizQuestion> quizQuestions);
    }
}