using QuizWebApplication.Models;

namespace QuizWebApplication.Services
{
    public interface IQuizRepository
    {
        bool PersistQuiz(Quiz quiz);
    }
}