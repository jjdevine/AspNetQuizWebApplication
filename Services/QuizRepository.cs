using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using QuizWebApplication.EntityFramework;
using QuizWebApplication.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizWebApplication.Services
{
    public class QuizRepository : IQuizRepository
    {
        private readonly IConfiguration Configuration;

        private readonly QuizContext QuizContext;

        public QuizRepository(IConfiguration configuration, QuizContext quizContext)
        {
            Configuration = configuration;
            this.QuizContext = quizContext;
        }

        public bool PersistQuiz(Quiz quiz)
        {
            Console.WriteLine(quiz);

            //delete the quiz if it already exists so we can recreate it
            DeleteQuiz(quiz.Id);

            QuizContext.Quizzes.Add(quiz);
            QuizContext.SaveChanges();

            return true;
        }

        public bool DeleteQuiz(Guid quizId)
        {
            QuizContext.QuizQuestions.Where(q => q.QuizId.Equals(quizId)).DeleteFromQuery();
            QuizContext.Quizzes.Where(q => q.Id.Equals(quizId)).DeleteFromQuery();
            QuizContext.SaveChanges();
            return true;
        }

        public bool PersistQuizQuestions(List<QuizQuestion> quizQuestions)
        {
            QuizContext.QuizQuestions.AddRange(quizQuestions);
            QuizContext.SaveChanges();
            return true;
        }

        public List<Quiz> LoadQuizzesForUser(string username)
        {
            return QuizContext.Quizzes.Where(q => q.Username.ToLower().Equals(username.ToLower())).ToList();
        }

        public Quiz LoadQuizById(Guid quizId)
        {
            Quiz quiz = QuizContext.Quizzes.Find(quizId);
           
            if(quiz == null) {
                throw new ArgumentException($"Quiz with ID [{quizId}] was not found in the Database");
            }

            return quiz;
        }

        public List<QuizQuestion> LoadQuizQuestions(Guid quizId, int listSize, bool randomOrder)
        {
            IQueryable<QuizQuestion> baseQuery = QuizContext.QuizQuestions.Where(q => q.QuizId.Equals(quizId));
            IOrderedQueryable<QuizQuestion> orderedQuery;
            IQueryable<QuizQuestion> finalQuery;

            //order by "order" field or randomly
            if (!randomOrder)
            {
                orderedQuery = baseQuery.OrderBy(q => q.Order);
            }
            else
            {
                orderedQuery = baseQuery.OrderBy(q => Guid.NewGuid());
            }

            //take a specific number of records or all records
            if(listSize > 0)
            {
                finalQuery = orderedQuery.Take(listSize);
            }
            else
            {
                finalQuery = orderedQuery;
            }

            return finalQuery.ToList();
        }
    }


}
